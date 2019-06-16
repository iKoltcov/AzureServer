namespace Client.Entities
{
    public class PingServerResult
    {
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public string InstanceId { get; set; }
        
        public string Ping { get; set; } 
    }
}