using GameChat.Models;
using System.Collections.Concurrent;

namespace GameChat.Handlers
{
    public class ChatHandlers
    {
        private static readonly List<GeneralMessage> _generalMessages = new List<GeneralMessage>();
        private static readonly List<SpecialChatRoom> _specialChatRooms = new List<SpecialChatRoom>();

        public static GeneralMessage AddGeneralMessage(string userId, string message,string color)
        {
            GeneralMessage generalMessage = new GeneralMessage();
            generalMessage.userId = userId;
            generalMessage.userColor = color;
            generalMessage.message = message;
            generalMessage.datetime = DateTime.Now;

            _generalMessages.Add(generalMessage);

            return generalMessage;
        }

        public static List<GeneralMessage> GetMessageInChat()
        {
            var messages = _generalMessages.OrderByDescending(x=>x.datetime).Take(100).Reverse().ToList();
            return messages;
        }

        public static SpecialMessage AddSpecialMessage(User user, User user2, string message)
        {
            SpecialMessage specialMessage = new SpecialMessage();
            specialMessage.chatRoomId = user.CookieId + "-" + user2.CookieId;
            specialMessage.fromCookieId = user.CookieId;
            specialMessage.toCookieId = user2.CookieId;
            specialMessage.fromUserId = user.UserId.ToString();
            specialMessage.toUserId = user2.UserId.ToString();
            specialMessage.fromColor=user.Color;
            specialMessage.toColor=user2.Color;
            specialMessage.message = message;
            specialMessage.datetime = DateTime.Now;

            SpecialChatRoom chatRoom = _specialChatRooms.Where(x => x.chatRoomId == (user.CookieId + "-" + user2.CookieId) || x.chatRoomId == (user2.CookieId + "-" + user.CookieId)).FirstOrDefault();

            if (chatRoom!=null)
            {
                if (chatRoom.roomMessages!=null)
                {
                    chatRoom.roomMessages.Add(specialMessage);
                }
                else
                {
                    chatRoom.roomMessages = new List<SpecialMessage>();
                    chatRoom.roomMessages.Add(specialMessage);
                }

            }
            else
            {
                chatRoom = new SpecialChatRoom();
                chatRoom.chatRoomId = user.CookieId + "-" + user2.CookieId;
                chatRoom.roomMessages= new List<SpecialMessage>();
                chatRoom.roomMessages.Add(specialMessage);

                _specialChatRooms.Add(chatRoom);
            }

            return specialMessage;
        }

        public static List<SpecialChatRoom> GetChatRooms(string cookieId)
        {
            var chatRooms = _specialChatRooms.Where(x=>x.chatRoomId.StartsWith(cookieId+"-") || x.chatRoomId.EndsWith("-"+cookieId)).ToList();
            return chatRooms;
        }
    }
}
