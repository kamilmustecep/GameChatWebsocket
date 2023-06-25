using GameChat.Models;
using System.Collections.Concurrent;

namespace GameChat.Handlers
{
    public static class UserHandler
    {
        private static readonly List<User> _connectionsHub = new List<User>();

        public static void AddUser(User user)
        {
            _connectionsHub.Add(user);
        }

        public static void RemoveUser(string connectionId)
        {
            var user = _connectionsHub.FirstOrDefault(x => x.ConnectionId == connectionId);
            _connectionsHub.Remove(user);
        }

        public static User GetUserConnectionId(string connectionId)
        {
            var user = _connectionsHub.FirstOrDefault(x=>x.ConnectionId== connectionId);
            return user;
        }

        public static User GetUserCookieId(string cookieId)
        {
            var user = _connectionsHub.FirstOrDefault(x => x.CookieId == cookieId);
            return user;
        }

        public static User GetUserUserId(int userId)
        {
            var user = _connectionsHub.FirstOrDefault(x => x.UserId == userId);
            return user;
        }

        public static User UpdateUserWithCookieId(string cookieId, string connectionId)
        {
            var user = _connectionsHub.FirstOrDefault(x => x.CookieId == cookieId);
            user.ConnectionId = connectionId;
            return user;
        }


        public static bool isUserIdAvailable(int userId)
        {
            var user = _connectionsHub.FirstOrDefault(x => x.UserId == userId);
            if (user!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isUserColorAvailable(string color)
        {
            var user = _connectionsHub.FirstOrDefault(x => x.Color == color);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetCookieIdWithConId(string connectionId)
        {
            string CookieId = _connectionsHub.FirstOrDefault(x => x.ConnectionId == connectionId).CookieId;
            return CookieId;
        }

        public static int GetOnlineUserCountInGeneralChat()
        {
            return _connectionsHub.Where(x=>x.isOnline==true).Count();
        }

        public static List<User> GetAllUserInGeneralChat()
        {
            return _connectionsHub.ToList();
        }
    }
}
