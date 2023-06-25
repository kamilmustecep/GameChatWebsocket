namespace GameChat.Models
{
    public class MainpageDTO
    {
        public User userProfile { get; set; }
        public int onlineUserCount { get; set; }
        public List<GeneralMessage> generalMessages { get; set; }
        public List<SpecialChatRoom> specialChatRooms { get; set; }
    }
}
