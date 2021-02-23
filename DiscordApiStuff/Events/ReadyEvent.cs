namespace DiscordApiStuff.Events
{
    public struct ReadyEvent: IDiscordEvent
    {
        public int op { get; set; } 
        
        public D d { get; set; } 
    }
    
    public struct D    
    {
        public int heartbeat_interval { get; set; } 
    }

}