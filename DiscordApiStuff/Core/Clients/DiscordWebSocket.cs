using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DiscordApiStuff.Core;
using DiscordApiStuff.Events.EventArgs.Gateway;
using DiscordApiStuff.Events.EventArgs.Message;
using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Exceptions.Gateway;
using DiscordApiStuff.Models.Enums;
using DiscordApiStuff.Models.Structs;
using DiscordApiStuff.Payloads.Connection;
using DiscordApiStuff.Payloads.Events;

namespace DiscordApiStuff.Core.Clients
{
    internal sealed class DiscordWebSocket
    {
        private GuildEventHandler _guildEvents;
        private ChannelEventHandler _channelEvents;
        private MemberEventHandler _memberEvents;
        private MessageEventHandler _messageEvents;
        private RoleEventHandler _roleEvents;
        private GatewayEventHandler _gatewayEvents;

        private ClientWebSocket _webSocket;
        private DiscordRestClient _discordRestClient;
        private CancellationTokenSource _cancellationTokenSource;
        private JsonSerializerOptions _defaultOptions;
        private Task _dataAccept;
        private Task _heartbeat;
        private DiscordClientConfiguration _discordClientConfiguration;
        private DateTime _lastHeartbeatAcknowledge;
        private int _heartbeatInterval;
        private int? _lastSequenceNumber;
        private string _sessionId;

        internal DiscordWebSocket(
            DiscordClientConfiguration discordClientConfiguration,
            GuildEventHandler guildEvents,
            ChannelEventHandler channelEvents,
            MemberEventHandler memberEvents,
            MessageEventHandler messageEvents,
            RoleEventHandler roleEvents,
            GatewayEventHandler gatewayEvents,
            DiscordRestClient discordRestClient
            )
        {
            _guildEvents = guildEvents;
            _channelEvents = channelEvents;
            _memberEvents = memberEvents;
            _messageEvents = messageEvents;
            _roleEvents = roleEvents;
            _gatewayEvents = gatewayEvents;

            _discordRestClient = discordRestClient;
            _webSocket = new ClientWebSocket();
            _defaultOptions = new JsonSerializerOptions() { WriteIndented = true };
            _discordClientConfiguration = discordClientConfiguration;

            _cancellationTokenSource = null;
            DefaultFields();
        }

        internal async Task ConnectAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _webSocket.ConnectAsync(new Uri(DiscordApiInfo.DiscordWebSocketGateway), _cancellationTokenSource.Token);
                Console.WriteLine("Connected.");
                _dataAccept = ListenForIncomingDataAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }
        internal void Disconnect()
        {
            try
            {
                _cancellationTokenSource.Cancel();
                _webSocket = new ClientWebSocket();
                DefaultFields();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }

        private void DefaultFields()
        {
            _heartbeat = null;
            _dataAccept = null;
            _lastHeartbeatAcknowledge = default;
            _heartbeatInterval = default;
            _lastSequenceNumber = null;
            _sessionId = string.Empty;
        }
        private async Task ListenForIncomingDataAsync()
        {
            byte[] buffer;

            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                buffer = new byte[25600]; //25 kb
                try
                {
                    var wsReceiveResult = await _webSocket.ReceiveAsync(buffer, _cancellationTokenSource.Token);
                    Console.WriteLine($"Receive Result: {wsReceiveResult.MessageType}");

                    if (!CheckCloseStatus(wsReceiveResult.CloseStatus))
                    {
                        break;
                    }

                    await HandleData(wsReceiveResult, buffer);
                }
                catch (WebSocketException e)
                {
                    break;
                }
                catch (OperationCanceledException e)
                {
                    break;
                }
            }

            Console.WriteLine("WebSocket stoppped listening");
        }

        private async Task HandleData(WebSocketReceiveResult wsReceiveResult, byte[] buffer)
        {
            switch (wsReceiveResult.MessageType)
            {
                case WebSocketMessageType.Text:
                    {
                        var payload = JsonSerializer.Deserialize<GeneralPayload<object>>(buffer.AsSpan(0, wsReceiveResult.Count));
                        _lastSequenceNumber = payload.Sequence;
                        Console.WriteLine($"Receive Result Payload: {payload.Code} | {payload.Sequence}");

                        switch (payload.Code)
                        {
                            case Opcode.Dispatch:
                                {
                                    ProcessDispatch(payload);
                                    break;
                                }
                            case Opcode.Heartbeat:
                                {
                                    await SendHeartbeatAsync();
                                    break;
                                }
                            case Opcode.Reconnect:
                                {
                                    await ReconnectAsync();
                                    break;
                                }
                            case Opcode.InvalidSession:
                                {
                                    bool sessionResumable = bool.Parse(payload.Data.ToString());
                                    if (sessionResumable)
                                    {
                                        _gatewayEvents.InvokeResuming();
                                        await ReconnectAsync();
                                    }
                                    break;
                                }
                            case Opcode.Hello:
                                {
                                    await IdentifyOrResumeAsync();
                                    ProcessHello(payload);
                                    break;
                                }
                            case Opcode.HeartbeatAck:
                                {
                                    _lastHeartbeatAcknowledge = DateTime.Now;
                                    break;
                                }
                        }
                    }
                    break;
                case WebSocketMessageType.Binary:
                    {
                        Console.WriteLine("WebSocket sent raw binary");
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ContinuousHeartbeatingAsync()
        {
            while (_webSocket.State == WebSocketState.Open)
            {
                //Check time difference, Timeout -> + 100 ms buffer
                TimeSpan differenceTimeSpan = DateTime.Now - TimeSpan.FromMilliseconds(_heartbeatInterval) - _lastHeartbeatAcknowledge;
                if (_lastHeartbeatAcknowledge != default && differenceTimeSpan.TotalMilliseconds > _heartbeatInterval + 100)
                {
                    Console.WriteLine("Heartbeat Acknowledge not received in time");
                    Disconnect();
                    break;
                }
                await SendHeartbeatAsync();
                await Task.Delay(_heartbeatInterval);
            }
        }

        private async Task IdentifyOrResumeAsync()
        {
            if (_sessionId == string.Empty)
            {
                Console.WriteLine("Identify received");
                var identificationPayload = new GeneralPayloadSlim<MinIdentification>()
                {
                    Code = Opcode.Identify,
                    Data = new MinIdentification()
                    {
                        Token = _discordClientConfiguration.Token,
                        Intents = (int)_discordClientConfiguration.Intents,
                        Property = DiscordClient.Properties
                    }
                };
                _gatewayEvents.InvokeIdentifying();
                await SendJsonDataAsync(identificationPayload);
                Console.WriteLine("Identify sent");
            }
            else
            {
                Console.WriteLine("Resume received");
                var resumePayload = new GeneralPayloadSlim<ResumePayload>()
                {
                    Code = Opcode.Identify,
                    Data = new ResumePayload()
                    {
                        Token = _discordClientConfiguration.Token,
                        Sequence = _lastSequenceNumber,
                        SessionId = _sessionId
                    }
                };
                _gatewayEvents.InvokeResuming();
                await SendJsonDataAsync(resumePayload);
                Console.WriteLine("Resume sent");
            }
        }
        private async Task SendHeartbeatAsync()
        {
            var heartbeat = new GeneralPayloadSlim<int?>() { Code = Opcode.Heartbeat, Data = _lastSequenceNumber };
            await SendJsonDataAsync(heartbeat);
            Console.WriteLine("Heartbeat sent");
            await Task.Delay(_heartbeatInterval);
        }
        private async Task ReconnectAsync()
        {
            _gatewayEvents.InvokeResuming();
            Disconnect();
            await ConnectAsync();
        }

        private void ProcessHello(GeneralPayload<object> payload)
        {
            Console.WriteLine("Hello received");
            var heartbeat = JsonSerializer.Deserialize<HeartbeatReceive>(payload.Data.ToString());
            _heartbeatInterval = heartbeat.HeartbeatInterval;
            _heartbeat = ContinuousHeartbeatingAsync();
        }
        private void ProcessDispatch(GeneralPayload<object> payload)
        {
            try
            {
                Console.WriteLine($"Event: {payload.Event}");

                switch (payload.Event)
                {
                    case "READY":
                        {
                            ReadyPayload ready = JsonSerializer.Deserialize<ReadyPayload>(payload.Data.ToString());
                            _sessionId = ready.SessionId;
                            _gatewayEvents.InvokeReady();
                            break;
                        }
                    //Guild
                    case "GUILD_CREATE":
                        {
                            break;
                        }
                    case "GUILD_DELETE":
                        {


                            break;
                        }
                    case "GUILD_ROLE_CREATE":
                        {


                            break;
                        }
                    case "GUILD_ROLE_UPDATE":
                        {


                            break;
                        }
                    case "GUILD_ROLE_DELETE":
                        {


                            break;
                        }
                    case "CHANNEL_CREATE":
                        {
                            break;
                        }
                    case "CHANNEL_UPDATE":
                        {
                            break;
                        }
                    case "CHANNEL_DELETE":
                        {
                            break;
                        }
                    case "CHANNEL_PINS_UPDATE":
                        {
                            break;
                        }
                    //Guild Members
                    case "GUILD_MEMBER_ADD":
                        {


                            break;
                        }
                    case "GUILD_MEMBER_UPDATE":
                        {


                            break;
                        }
                    case "GUILD_MEMBER_REMOVE":
                        {


                            break;
                        }
                    //Guild Bans
                    case "GUILD_BAN_ADD":
                        {


                            break;
                        }
                    case "GUILD_BAN_REMOVE":
                        {


                            break;
                        }
                    //Guild Emojis
                    case "GUILD_EMOJIS_UPDATE":
                        {


                            break;
                        }
                    //Guild Integrations
                    case "GUILD_INTEGRATIONS_UPDATE":
                        {


                            break;
                        }
                    //Guild Webhooks
                    case "WEBHOOKS_UPDATE":
                        {


                            break;
                        }
                    //Guild Invites
                    case "INVITE_CREATE":
                        {


                            break;
                        }
                    case "INVITE_DELETE":
                        {


                            break;
                        }
                    //Guild Voice States
                    case "VOICE_STATE_UPDATE":
                        {


                            break;
                        }
                    //Guild PRESENCE
                    case "PRESENCE_UPDATE":
                        {


                            break;
                        }
                    //Guild Message Reactions
                    case "MESSAGE_REACTION_ADD":
                        {


                            break;
                        }
                    case "MESSAGE_REACTION_REMOVE":
                        {


                            break;
                        }
                    case "MESSAGE_REACTION_REMOVE_ALL":
                        {


                            break;
                        }
                    case "MESSAGE_REACTION_REMOVE_EMOJI":
                        {


                            break;
                        }
                    //Guild / Direct Message Typing
                    case "TYPING_START":
                        {


                            break;
                        }
                    //Messages
                    case "MESSAGE_CREATE":
                        {
                            Console.WriteLine(payload.Data.ToString());
                            Message message = JsonSerializer.Deserialize<Message>(payload.Data.ToString());
                            var evArgs = new MessageCreatedEventArgs()
                            {
                                Message = message
                            };
                            _messageEvents.InvokeMessageCreated(evArgs);
                            break;
                        }
                    case "MESSAGE_UPDATE":
                        {


                            break;
                        }
                    case "MESSAGE_DELETE":
                        {


                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private bool CheckCloseStatus(WebSocketCloseStatus? closeStatus)
        {
            Exception exception = null;

            switch ((int?)closeStatus)
            {
                case 4003:
                    exception = new NotAuthenticatedException("You sent a payload prior to identifying");
                    break;
                case 4004:
                    exception = new AuthenticationFailedException(_discordClientConfiguration.Token, "Invalid Token");
                    break;
                case 4005:
                    exception = new AlreadyAuthenticatedException("You sent more than one identify payload");
                    break;
            }

            if (exception != null)
            {
                var eventArgs = new GatewayExceptionEventArgs(exception);
                _gatewayEvents.InvokeExceptionThrown(eventArgs);
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task SendJsonDataAsync<T>(T obj)
        {
            string jsonStr = JsonSerializer.Serialize(obj);
            //Console.WriteLine(jsonStr);
            byte[] data = Encoding.UTF8.GetBytes(jsonStr);
            await SendDataAsync(data);
        }
        private async Task SendDataAsync(byte[] data, WebSocketMessageType messageType = WebSocketMessageType.Text)
        {
            await _webSocket.SendAsync(data, messageType, true, _cancellationTokenSource.Token);
        }
    }
}