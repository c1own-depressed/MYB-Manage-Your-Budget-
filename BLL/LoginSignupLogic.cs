namespace BLL
{
    using System.Security.Cryptography;
    using System.Text;
    using DAL;

    public class LoginSignupLogic
    {
        public static User AddUser(string username, string email, string password)
        {
            string hashedPassword = HashString(password);
            User newUser = new User(username, email, hashedPassword, true, "ua", "uah");

            UserQueries.AddUser(newUser);

            return newUser;
        }

        public static string HashString(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static User UpdateUser(int userId, string language, bool isLightTheme, string currency)
        {
            return UserQueries.UpdateUser(userId, language, isLightTheme, currency);
        }

        public static User GetUser(int userId)
        {
            return UserQueries.GetUserById(userId);
        }

        public static User GetUserByUsername(string username)
        {
            return UserQueries.GetUserByUsername(username);
        }

        public static int CheckCredentials(string username, string password)
        {
            if (password.Length == 0 || username.Length == 0)
            {
                return 0;
            }

            string hashedPassword = HashString(password);

            User user = UserQueries.GetUserByUsername(username);
            if (user == null || user.HashedPassword != hashedPassword)
            {
                return 0;
            }
            else
            {
                return user.Id;
            }
        }

        public static bool EmailExists(string email)
        {
            User user = UserQueries.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
