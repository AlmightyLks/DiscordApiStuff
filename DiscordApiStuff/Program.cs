using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

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
                Console.WriteLine("Closed connection");
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
                //TimeSpan differenceTimeSpan = (DateTime.Now - TimeSpan.FromMilliseconds(_heartbeatInterval)) - _lastHeartbeatAcknowledge;
                //if (_lastHeartbeatAcknowledge != default && differenceTimeSpan.TotalMilliseconds > _heartbeatInterval + 100)
                //{
                //    Console.WriteLine("Heartbeat Acknowledge not received in time");
                //    await DisconnectAsync();
                //    break;
                //}
                await SendHeartbeatAsync();
                await Task.Delay(_heartbeatInterval);
            }
        }
        private async Task SendHeartbeatAsync()
        {
            var heartbeat = new { op = (byte)Opcode.Heartbeat, d = _lastSequenceNumber };
            await SendJsonDataAsync(heartbeat);
            Console.WriteLine(JsonSerializer.Serialize(heartbeat));
            Console.WriteLine("Heartbeat sent");
            await Task.Delay(_heartbeatInterval);
        }
        private async Task ListenForIncomingDataAsync()
        {
            byte[] buffer = new byte[1024];
            ArraySegment<byte> data = new ArraySegment<byte>(buffer);
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var wsReceiveResult = await _webSocket.ReceiveAsync(data, _cancellationToken);
                    Console.WriteLine($"Receive Result: {wsReceiveResult.MessageType}");

                    switch (wsReceiveResult.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            {
                                string jsonStr = Encoding.UTF8.GetString(data.Array, 0, wsReceiveResult.Count);
                                Payload payload = JsonSerializer.Deserialize<Payload>(jsonStr);
                                _lastSequenceNumber = payload.Sequence;
                                Console.WriteLine($"Receive Result Payload: {payload.Code}");
                                Console.WriteLine($"Receive Result Payload: {payload.Sequence}");

                                switch (payload.Code)
                                {
                                    case Opcode.Dispatch:
                                        {
                                            ProcessDispatch(payload);
                                        }
                                        break;
                                    case Opcode.Heartbeat:
                                        {
                                            await SendHeartbeatAsync();
                                        }
                                        break;
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
                                    default:
                                        break;
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
            }
        }

        private async Task SendIdentity(Payload payload)
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

        private void ProcessHello(Payload payload)
        {
            Console.WriteLine("Hello received");
            var heartbeat = JsonSerializer.Deserialize<HeartbeatHello>(payload.Data.ToString());
            _heartbeatInterval = heartbeat.HeartbeatInterval;
            _heartbeat = ContinuousHeartbeatingAsync();

            //Wot
            Console.WriteLine("Hello sent");
        }
        private void ProcessDispatch(Payload payload)
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
                            ReadyData readyData = JsonSerializer.Deserialize<ReadyData>(payload.Data.ToString());
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
        public DiscordClientConfiguration(string token, DiscordIntent intents)
        {
            Token = token;
            Intents = intents;
        }
    }
    public struct MinIdentification
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public AllData Data { get; set; }
        public struct AllData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
            [JsonPropertyName("intents")]
            public int Intents { get; set; }
            [JsonPropertyName("properties")]
            public Properties Properties { get; set; }
        }
        public struct Properties
        {
            [JsonPropertyName("$os")]
            public string OperatingSystem { get; set; }
            [JsonPropertyName("$browser")]
            public string Browser { get; set; }
            [JsonPropertyName("$device")]
            public string Device { get; set; }
        }
    }
    public struct Identification
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public AllData Data { get; set; }
        public struct AllData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
            [JsonPropertyName("intents")]
            public int Intents { get; set; }
            [JsonPropertyName("properties")]
            public Properties Property { get; set; }
            [JsonPropertyName("compress")]
            public bool? Compress { get; set; }
            [JsonPropertyName("large_threshold")]
            public int? LargeThreshold { get; set; }
            [JsonPropertyName("shard")]
            public int[] Shard { get; set; }
            [JsonPropertyName("presence")]
            public Presence? Presence { get; set; }
        }
        public struct Presence
        {
            [JsonPropertyName("activities")]
            public Activity[] Activities { get; set; }
            [JsonPropertyName("status")]
            public string Status { get; set; }
            [JsonPropertyName("since")]
            public int? Since { get; set; }
            [JsonPropertyName("afk")]
            public bool Afk { get; set; }
        }
        public struct Activity
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("type")]
            public ActivityType Type { get; set; }
            [JsonPropertyName("url")]
            public string URL { get; set; }
        }
        public struct Properties
        {
            [JsonPropertyName("$os")]
            public string OperatingSystem { get; set; }
            [JsonPropertyName("$browser")]
            public string Browser { get; set; }
            [JsonPropertyName("$device")]
            public string Device { get; set; }
        }
    }
    public struct Payload
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public object Data { get; set; }
        [JsonPropertyName("s")]
        public int? Sequence { get; set; }
        [JsonPropertyName("t")]
        public string Event { get; set; }
    }
    public struct HeartbeatHello
    {
        [JsonPropertyName("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
    }
    public struct ReadyData
    {
        [JsonPropertyName("v")]
        public int GatewayVersion { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("private_channels")]
        public ulong[] PrivateChannels { get; set; }

        //Create unavailable guild type
        //https://discord.com/developers/docs/resources/guild#unavailable-guild-object
        [JsonPropertyName("guilds")]
        public object[] Guilds { get; set; } 
        [JsonPropertyName("shard")]
        public int[] Shards { get; set; }
        //Create application object type
        //https://discord.com/developers/docs/topics/oauth2#application-object
        [JsonPropertyName("application")]
        public object Application { get; set; } 
    }
    public struct User
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("discriminator")]
        public string Discriminator { get; set; }
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
        [JsonPropertyName("bot")]
        public bool? IsBot { get; set; }
        [JsonPropertyName("system")]
        public bool? DiscordUser { get; set; }
        [JsonPropertyName("mfa_enabled")]
        public bool? MultiFactorAuthentication { get; set; }
        [JsonPropertyName("locale")]
        public string LocalLanguage { get; set; }
        [JsonPropertyName("verified")]
        public bool? EMailVerified { get; set; }
        [JsonPropertyName("email")]
        public string EMail { get; set; }
        [JsonPropertyName("flags")]
        public UserFlags? Flags { get; set; }
        [JsonPropertyName("premium_type")]
        public Nitro Nitro { get; set; }
        [JsonPropertyName("public_flags")]
        public UserFlags? PublicFlags { get; set; }
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
