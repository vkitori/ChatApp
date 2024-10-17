namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Content { get; set; }
        public DateTime Timestamp{ get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public int ChatRoomId { get; set; } 
        public ChatRoom ChatRoom { get; set; } 

        public Message()
        {
            Content = "";
            ChatRoomId = 1;
        }
    }
}
