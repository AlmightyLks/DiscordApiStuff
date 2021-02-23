﻿using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Exceptions;
using DiscordApiStuff.Payloads.Gateway;

namespace DiscordApiStuff
{
    internal class DiscordWebSocket
    {
        private GuildEventHandler _guildEvents;
        private ChannelEventHandler _channelEvents;
        private MemberEventHandler _memberEvents;
        private MessageEventHandler _messageEvents;
        private RoleEventHandler _roleEvents;

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

        internal DiscordWebSocket(
            DiscordClientConfiguration discordClientConfiguration,
            CancellationToken cancellationToken,
            GuildEventHandler guildEvents,
            ChannelEventHandler channelEvents,
            MemberEventHandler memberEvents,
            MessageEventHandler messageEvents,
            RoleEventHandler roleEvents
            )
        {
            _guildEvents = guildEvents;
            _channelEvents = channelEvents;
            _memberEvents = memberEvents;
            _messageEvents = messageEvents;
            _roleEvents = roleEvents;

            _webSocket = new ClientWebSocket();
            _defaultOptions = new JsonSerializerOptions() { WriteIndented = true };
            _cancellationToken = cancellationToken;
            _discordClientConfiguration = discordClientConfiguration;
            _heartbeat = null;
            _heartbeatInterval = 0;
            _lastSequenceNumber = null;
            _lastHeartbeatAcknowledge = default;
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

                    //This is where we call the processor

                    switch (wsReceiveResult.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            {
                                var payload = JsonSerializer.Deserialize<GeneralPayload>(buffer.AsSpan(0, wsReceiveResult.Count));
                                _lastSequenceNumber = payload.Sequence;
                                Console.WriteLine($"Receive Result Payload: {payload.Code}");
                                Console.WriteLine($"Receive Result Payload: {payload.Sequence}");

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
                                            break;
                                        }
                                    case Opcode.InvalidSession:
                                        {
                                            throw new InvalidOrEmptyTokenException(_discordClientConfiguration.Token, $"\"{nameof(_discordClientConfiguration.Token)}\" is invalid.");
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
                            }
                            break;
                        case WebSocketMessageType.Close:
                            {
                                Console.WriteLine($"WebSocket Close Signal Received\n{wsReceiveResult.CloseStatusDescription}");
                                break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (WebSocketException e)
                {
                    Console.WriteLine($"{e}");
                    break;
                }
                catch (InvalidOrEmptyTokenException e)
                {
                    Console.WriteLine($"{e}");
                    break;
                }
                catch (JsonException e)
                {

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e}");
                }
            }
            Console.WriteLine("WebSocket stoppped listening");
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
            if (string.IsNullOrWhiteSpace(_discordClientConfiguration.Token))
            {
                throw new InvalidOrEmptyTokenException(_discordClientConfiguration.Token, $"{nameof(_discordClientConfiguration.Token)} is either empty, null or consists only of whitespaces");
            }

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
                            ReadyEventArgs readyEventArgs = JsonSerializer.Deserialize<ReadyEventArgs>(payload.Data.ToString());
                        }
                        break;
                    case "MESSAGE_CREATE":
                        {
                            //Console.WriteLine($"Message created");
                        }
                        break;
                    case "MESSAGE_UPDATE":
                        {
                            //Console.WriteLine($"Message updated");
                        }
                        break;
                    case "MESSAGE_DELETE":
                        {
                            //Console.WriteLine($"Message deleted");
                        }
                        break;
                }
            }
            catch (Exception e)
            {

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