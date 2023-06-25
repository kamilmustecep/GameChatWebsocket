namespace GameChat.Models
{
    public class GeneralMessage
    {
        public string userId { get; set; } 
        public string message { get; set;}

        public string userColor { get; set; }
        public DateTime datetime { get; set; }
    }

    public class SpecialMessage
    {
        //fromCookieId-toCookieId
        public string chatRoomId { get; set; }
        public string fromCookieId { get; set; }
        public string toCookieId { get; set; }
        public string fromUserId { get; set; }
        public string toUserId { get; set; }
        public string fromColor { get; set; }
        public string toColor { get; set; }
        public string message { get; set; }
        public DateTime datetime { get; set; }
    }


    public class SpecialChatRoom
    {
        //fromCookieId-toCookieId
        public string chatRoomId { get; set; }
        public List<SpecialMessage> roomMessages { get; set; }
    }

}
