using System.Security.Cryptography;
using System.Text.RegularExpressions;
using GameChat.Handlers;
using GameChat.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace GameChat.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
       
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("CreateAndSendCookieId");
            await base.OnConnectedAsync();
        }
        
        
        public async Task ReceiveUserCookieId(string cookieId)
        {
            User user = UserHandler.GetUserCookieId(cookieId);

            if (user!=null)
            {
                user.ConnectionId = Context.ConnectionId;
                user.isOnline = true;
            }
            else
            {
                var userId = Helpers.GenerateUserId();

                user = new User
                {
                    ConnectionId = Context.ConnectionId,
                    isOnline = true,
                    UserId = userId,
                    CookieId = cookieId,
                    Color = Helpers.GenerateRandomColorCode()

                };

                UserHandler.AddUser(user);
            }


            //Ana chat mesajları son 100 mesaj.
            var generalMessages = ChatHandlers.GetMessageInChat();
            var chatRooms = ChatHandlers.GetChatRooms(cookieId);
            var onlineUserCount = UserHandler.GetOnlineUserCountInGeneralChat();

            MainpageDTO mainpageDTO = new MainpageDTO();
            mainpageDTO.userProfile = user;
            mainpageDTO.specialChatRooms = chatRooms;
            mainpageDTO.generalMessages = generalMessages;
            mainpageDTO.onlineUserCount= onlineUserCount;


            string serializedModel = JsonConvert.SerializeObject(mainpageDTO);
            Clients.Client(Context.ConnectionId).SendAsync("JoinGameChat", serializedModel);

            Clients.AllExcept(Context.ConnectionId).SendAsync("InhanceOnlineUser");
        }


        public async Task SendGeneralMessage(string message)
        {
            User user = UserHandler.GetUserConnectionId(Context.ConnectionId);

            if (user != null)
            {
                var generalMessageModel = ChatHandlers.AddGeneralMessage(user.UserId.ToString(), message,user.Color);

                string serializedModel = JsonConvert.SerializeObject(generalMessageModel);

                Clients.All.SendAsync("ReceiveGeneralMessage", serializedModel);
            }

        }


        public async Task SendSpecialMessage(string message,int userId)
        {
            User user = UserHandler.GetUserConnectionId(Context.ConnectionId);
            User user2 = UserHandler.GetUserUserId(userId);

            if (user != null && user2!=null)
            {
                var specialMessageModel = ChatHandlers.AddSpecialMessage(user, user2,message);

                string serializedModel = JsonConvert.SerializeObject(specialMessageModel);

                Clients.Clients(user.ConnectionId,user2.ConnectionId).SendAsync("ReceiveSpecialMessage", serializedModel);
            }

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = UserHandler.GetUserConnectionId(Context.ConnectionId);
            if (user != null)
            {
                user.isOnline = false;
            }

            Clients.AllExcept(Context.ConnectionId).SendAsync("ReduceOnlineUser");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
