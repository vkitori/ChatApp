namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public User User { get; set; }

        public Message()
        {
            Content = "";
        }
    }
}
