﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DiscordApiStuff.Payloads.Gateway;

namespace DiscordApiStuff
{
    class Program
    {
        static async Task Main()
        {
            DiscordClient client = new DiscordClient(new DiscordClientConfiguration()
            {
                Token = "ODEyNzU4MjYxNzkwNDA4NzI0.YDFaHQ.6YeNQbfOcqiS4Ns0dkCdpUJ1fhk",
                Intents = DiscordIntent.AllUnprivileged
            });
            await client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
    class DiscordClient
    {
        private CancellationTokenSource _cancellationTokenSource;
        private DiscordClientConfiguration _discordClientConfiguration;
        private DiscordWebSocket _discordWebSocket;
        internal static readonly MinIdentification.Properties Properties;
        static DiscordClient()
        {
            Properties = new MinIdentification.Properties()
            {
                OperatingSystem = GetOsType(),
                Browser = "Wholesome",
                Device = "Wholesome"
            };

            static string GetOsType()
            {
                PlatformID pid = Environment.OSVersion.Platform;
                switch (pid)
                {
                    case PlatformID.Win32NT:
                        return "windows";
                    case PlatformID.Unix:
                        return "unix";
                    default:
                        return "unknown";
                }
            }
        }

        public DiscordClient(DiscordClientConfiguration discordClientConfiguration)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _discordClientConfiguration = discordClientConfiguration;
            _discordWebSocket = new DiscordWebSocket(_discordClientConfiguration, _cancellationTokenSource.Token);
        }

        public async Task ConnectAsync()
        {
            try
            {
                await _discordWebSocket.ConnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }
        public async Task DisconnectAsync()
        {
            try
            {
                await _discordWebSocket.DisconnectAsync();
                _cancellationTokenSource.Cancel();
                Console.WriteLine("Closed connection");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }
    }
    internal class DiscordWebSocket
    {
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

        internal DiscordWebSocket(DiscordClientConfiguration discordClientConfiguration, CancellationToken cancellationToken)
        {
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
        private async Task SendHeartbeatAsync()
        {
            var heartbeat = new HeartbeatSend() { Code = Opcode.Heartbeat, Data = _lastSequenceNumber };
            await SendJsonDataAsync(heartbeat);
            Console.WriteLine(JsonSerializer.Serialize(heartbeat));
            Console.WriteLine("Heartbeat sent");
            await Task.Delay(_heartbeatInterval);
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
                            
                            //string jsonStr = Encoding.UTF8.GetString(buffer, 0, wsReceiveResult.Count);
                            
                            //Payload payload = JsonSerializer.Deserialize<Payload>(jsonStr);
                            
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

                                        }
                                        break;
                                    case Opcode.InvalidSession:
                                        {
                                            throw new InvalidOrEmptyTokenException(_discordClientConfiguration.Token, $"\"{nameof(_discordClientConfiguration.Token)}\" is invalid.");
                                        }
                                    case Opcode.Hello:
                                        {
                                            await SendIdentity(payload);
                                            ProcessHello(payload);
                                        }
                                        break;
                                    case Opcode.HeartbeatAck:
                                        {
                                            _lastHeartbeatAcknowledge = DateTime.Now;
                                        }
                                        break;
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
                                //await _webSocket.CloseAsync(_webSocket.CloseStatus.Value, _webSocket.CloseStatusDescription, _cancellationTokenSource.Token);
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
                    //Console.WriteLine($"{e}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e}");
                }
            }
            Console.WriteLine("-------------------------------- I left.");
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
                            ReadyEvent readyEvent = JsonSerializer.Deserialize<ReadyEvent>(payload.Data.ToString());
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

    public struct DiscordClientConfiguration
    {
        public string Token { get; init; }
        public DiscordIntent Intents { get; init; }
        public bool AutoReconnect { get; init; }
        public DiscordClientConfiguration(string token, DiscordIntent intents, bool autoReconnect)
        {
            Token = token;
            Intents = intents;
            AutoReconnect = autoReconnect;
        }
    }



    public enum ActivityType : byte
    {
        Playing,
        Streaming,
        Listening,
        //3 non-existent
        Custom = 4,
        Competing
    }
    public enum Opcode : byte
    {
        Dispatch,
        Heartbeat,
        Identify,
        PresenceUpdate,
        VoiceStateUpdate,
        //5 non-existent
        Resume = 6,
        Reconnect,
        RequestGuildMembers,
        InvalidSession,
        Hello,
        HeartbeatAck
    }

    public enum Nitro
    {
        None,
        NitroClassic,
        Nitro
    }

    [Flags]
    public enum UserFlags
    {
        None = 0 << 0,

        DiscordEmployee = 1 << 0,

        PartneredServerOwner = 1 << 1,

        HypeSquadEvents = 1 << 2,

        BugHunterLevel1 = 1 << 3,

        // 1 << 4 non-existent

        // 1 << 5 non-existent

        HouseBravery = 1 << 6,

        HouseBrilliance = 1 << 7,

        HouseBalance = 1 << 8,

        EarlySupporter = 1 << 9,

        TeamUser = 1 << 10,

        // 1 << 11 non-existent

        System = 1 << 12,

        // 1 << 13 non-existent

        BugHunterLevel2 = 1 << 14,

        // 1 << 15 non-existent

        VerifiedBot = 1 << 16,

        EarlyVerifiedBotDeveloper = 1 << 17
    }
    [Flags]
    public enum DiscordIntent
    {
        None = 0 << 0,

        Guilds = 1 << 0,

        GuildMembers = 1 << 1,

        GuildBan = 1 << 2,

        GuildEmojis = 1 << 3,

        GuildIntegrations = 1 << 4,

        GuildWebhooks = 1 << 5,

        GuildInvites = 1 << 6,

        GuildVoiceChat = 1 << 7,

        GuildPresence = 1 << 8,

        GuildMessages = 1 << 9,

        GuildReactions = 1 << 10,

        GuildTyping = 1 << 11,

        DirectMessages = 1 << 12,

        DirectMessageReaction = 1 << 13,

        DirectMessageTyping = 1 << 14,

        AllUnprivileged = Guilds | GuildBan | GuildEmojis | GuildIntegrations |
            GuildWebhooks | GuildInvites | GuildVoiceChat | GuildMessages |
            GuildReactions | GuildTyping | DirectMessages | DirectMessageReaction | DirectMessageTyping,

        All = AllUnprivileged | GuildMembers | GuildPresence
    }

    public sealed class InvalidOrEmptyTokenException : Exception
    {
        public string InvalidToken { get; }
        internal InvalidOrEmptyTokenException(string invalidToken, string message) : base(message)
        {
            InvalidToken = invalidToken;
        }
    }
    internal sealed class DiscordApiInfo
    {
        //Easy configurable, easy to find
        public static readonly string DiscordApiGatewayVersion = "8";
        public static readonly string DiscordApiEncoding = "json";

        //Easy to interpolate
        public static readonly string DiscordWebSocketGateway_V8 = $"wss://gateway.discord.gg/?v={DiscordApiGatewayVersion}&encoding={DiscordApiEncoding}";
    }
}
