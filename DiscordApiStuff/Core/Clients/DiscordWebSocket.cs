using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DiscordApiStuff.Core;
using DiscordApiStuff.Events.EventArgs.Gateway;
using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Exceptions.Gateway;
using DiscordApiStuff.Payloads.Events;
using DiscordApiStuff.Payloads.Gateway.Connection;
using DiscordApiStuff.Payloads.Models.Enums;

namespace DiscordApiStuff
{
    internal class DiscordWebSocket
    {
        private GuildEventHandler _guildEvents;
        private ChannelEventHandler _channelEvents;
        private MemberEventHandler _memberEvents;
        private MessageEventHandler _messageEvents;
        private RoleEventHandler _roleEvents;
        private GatewayEventHandler _gatewayEvents;

        private JsonSerializerOptions _defaultOptions;
        private ClientWebSocket _webSocket;
        private Task _dataAccept;
        private Task _heartbeat;
        private Stopwatch _stopwatch;
        private CancellationToken _cancellationToken;
        private DiscordClientConfiguration _discordClientConfiguration;
        private DateTime _lastHeartbeatAcknowledge;
        private int _heartbeatInterval;
        private int? _lastSequenceNumber;
        private string _sessionId;
        private bool _connected;

        internal DiscordWebSocket(
            DiscordClientConfiguration discordClientConfiguration,
            CancellationToken cancellationToken,
            GuildEventHandler guildEvents,
            ChannelEventHandler channelEvents,
            MemberEventHandler memberEvents,
            MessageEventHandler messageEvents,
            RoleEventHandler roleEvents,
            GatewayEventHandler gatewayEvents
            )
        {
            _guildEvents = guildEvents;
            _channelEvents = channelEvents;
            _memberEvents = memberEvents;
            _messageEvents = messageEvents;
            _roleEvents = roleEvents;
            _gatewayEvents = gatewayEvents;

            _webSocket = new ClientWebSocket();
            _defaultOptions = new JsonSerializerOptions() { WriteIndented = true };
            _cancellationToken = cancellationToken;
            _discordClientConfiguration = discordClientConfiguration;
            _heartbeat = null;
            _lastHeartbeatAcknowledge = default;
            _heartbeatInterval = 0;
            _lastSequenceNumber = null;
            _sessionId = string.Empty;
            _connected = false;
        }

        internal async Task ConnectAsync()
        {
            try
            {
                await _webSocket.ConnectAsync(new Uri(DiscordApiInfo.DiscordWebSocketGateway_V8), _cancellationToken);
                Console.WriteLine("Connected.");
                _stopwatch = Stopwatch.StartNew();
                _dataAccept = ListenForIncomingDataAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }
        internal async Task DisconnectAsync(WebSocketCloseStatus socketCloseStatus = WebSocketCloseStatus.NormalClosure, string closeStatusDescription = "Disconnect")
        {
            try
            {
                await _webSocket.CloseAsync(socketCloseStatus, closeStatusDescription, _cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }

        private async Task ListenForIncomingDataAsync()
        {
            byte[] buffer;

            while (!_cancellationToken.IsCancellationRequested)
            {
                buffer = new byte[51200];
                try
                {
                    var wsReceiveResult = await _webSocket.ReceiveAsync(buffer, _cancellationToken);
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
                        var payload = JsonSerializer.Deserialize<GeneralPayload>(buffer.AsSpan(0, wsReceiveResult.Count));
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
                                    _connected = false;
                                    await ReconnectAsync();
                                    break;
                                }
                            case Opcode.InvalidSession:
                                {
                                    _connected = false;
                                    //Not working yet.
                                    var smth = bool.Parse(payload.Data.ToString());
                                    break;
                                }
                            case Opcode.Hello:
                                {
                                    await SendIdentity(payload);
                                    ProcessHello(payload);
                                    break;
                                }
                            case Opcode.HeartbeatAck:
                                {
                                    _lastHeartbeatAcknowledge = DateTime.Now;
                                    break;
                                }
                            case Opcode.Identify:
                                break;
                            case Opcode.PresenceUpdate:
                                break;
                            case Opcode.VoiceStateUpdate:
                                break;
                            case Opcode.Resume:
                                break;
                            case Opcode.RequestGuildMembers:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
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
                TimeSpan differenceTimeSpan = (DateTime.Now - TimeSpan.FromMilliseconds(_heartbeatInterval)) - _lastHeartbeatAcknowledge;
                if (_lastHeartbeatAcknowledge != default && differenceTimeSpan.TotalMilliseconds > _heartbeatInterval + 100)
                {
                    Console.WriteLine("Heartbeat Acknowledge not received in time");
                    await DisconnectAsync();
                    break;
                }
                await SendHeartbeatAsync();
                await Task.Delay(_heartbeatInterval);
            }
        }

        private async Task SendIdentity(GeneralPayload payload)
        {
            Console.WriteLine("Identify received");

            MinIdentification identification = new MinIdentification()
            {
                Code = Opcode.Identify,
                Data = new MinIdentification.AllData()
                {
                    Token = _discordClientConfiguration.Token,
                    Intents = (int)_discordClientConfiguration.Intents,
                    Properties = DiscordClient.Properties
                }
            };

            //Wot
            await SendJsonDataAsync(identification);
            Console.WriteLine("Identify sent");
        }
        private async Task SendHeartbeatAsync()
        {
            var heartbeat = new HeartbeatSend() { Code = Opcode.Heartbeat, Data = _lastSequenceNumber };
            await SendJsonDataAsync(heartbeat);
            Console.WriteLine(JsonSerializer.Serialize(heartbeat));
            Console.WriteLine("Heartbeat sent");
            await Task.Delay(_heartbeatInterval);
        }
        private async Task ReconnectAsync()
        {
            //_gatewayEvents.InvokeReconnect();
        }

        private void ProcessHello(GeneralPayload payload)
        {
            Console.WriteLine("Hello received");
            var heartbeat = JsonSerializer.Deserialize<HeartbeatReceive>(payload.Data.ToString());
            _heartbeatInterval = heartbeat.HeartbeatInterval;
            _heartbeat = ContinuousHeartbeatingAsync();

            //Wot
            Console.WriteLine("Hello sent");
        }
        private void ProcessDispatch(GeneralPayload payload)
        {
            try
            {
                Console.WriteLine($"Event: {payload.Event}");

                switch (payload.Event)
                {
                    case "READY":
                        {
                            _stopwatch.Stop();
                            Console.WriteLine($"Connect to Ready: {_stopwatch.Elapsed.TotalMilliseconds} ms");
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

            if(exception != null)
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
            byte[] data = Encoding.UTF8.GetBytes(jsonStr);
            await SendDataAsync(data);
        }
        private async Task SendDataAsync(byte[] data, WebSocketMessageType messageType = WebSocketMessageType.Text)
        {
            await _webSocket.SendAsync(data, messageType, true, _cancellationToken);
        }
    }
}