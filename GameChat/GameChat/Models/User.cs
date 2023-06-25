namespace GameChat.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public int UserId { get; set; }
        public bool isOnline { get; set; }
        public string CookieId { get; set; }
        public string Color { get; set; }

    }
}
