using GameChat.Handlers;

namespace GameChat
{
    public class Helpers
    {
        public static int GenerateUserId()
        {
            x:
            Random random = new Random();
            int randomNumber = random.Next(1000, 100001);

            bool isUserIdAvailable = UserHandler.isUserIdAvailable(randomNumber);

            if (!isUserIdAvailable)
            {
                return randomNumber;
            }
            else
            {
                goto x;
            }
        }

        public static string GenerateRandomColorCode()
        {
            x:
            Random random = new Random();
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);

            string colorCode = "#" + BitConverter.ToString(colorBytes).Replace("-", string.Empty);

            bool isUserColorAvailable = UserHandler.isUserColorAvailable(colorCode);

            if (!isUserColorAvailable)
            {
                return colorCode;
            }
            else
            {
                goto x;
            }
        }
    }
}
