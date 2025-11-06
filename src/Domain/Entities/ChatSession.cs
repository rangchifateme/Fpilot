namespace Domain.Entities
{
    public class ChatSession
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        
        public string UserId { get; set; } = ""; 
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
