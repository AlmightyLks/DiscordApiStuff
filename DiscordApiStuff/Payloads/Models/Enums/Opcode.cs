namespace DiscordApiStuff.Payloads.Models.Enums
{
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
}