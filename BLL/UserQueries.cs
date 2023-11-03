using DAL;
using Org.BouncyCastle.Crypto.Generators;

namespace BLL
{
    public class UserQueries
    {
        static public User AddUser(string username, string email, string password)
        {
            // Install-Package BCrypt.Net
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
            string hashedPassword = password;
            User newUser = new User
            {
                Username = username,
                Email = email,
                HashedPassword = hashedPassword,
                LightTheme = true,
                Language = "ua",
                Currency = "uah",
            };

            Queries.AddUser(newUser);

            return newUser;
        }

        static public User GetUser(int userId)
        {
            return Queries.GetUserById(userId);
        }

        static public User GetUserByUsername(string username)
        {
            return Queries.GetUserByUsername(username);
        }

        static public int CheckCredentials(string username, string password)
        {
            if (password.Length == 0 || username.Length == 0)
            {
                return 0;
            }

            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
            string hashedPassword = password;

            User user = Queries.GetUserByUsername(username);
            if (user == null || user.HashedPassword != hashedPassword)
            {
                return 0;
            }
            else
            {
                return user.Id;
            }
        }

        static public bool EmailExists(string email)
        {
            User user = Queries.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}