namespace Domain.Entities
{
    public class ChatMessage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        
        public string SessionId { get; set; } = ""; 
        
        public string UserId { get; set; } = ""; 
        
        public string Question { get; set; } = ""; 
        
        public string Answer { get; set; } = ""; 
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
