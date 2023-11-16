using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoginSignupLogic
    {
        static public User AddUser(string username, string email, string password)
        {
            // Install-Package BCrypt.Net
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
            string hashedPassword = password;
            User newUser = new User(username, email, hashedPassword, true, "ua", "uah");

            UserQueries.AddUser(newUser);

            return newUser;
        }

        static public User UpdateUser(int userId, string language, bool isLightTheme, string currency)
        {
            return UserQueries.UpdateUser(userId, language, isLightTheme, currency);
        }

        static public User GetUser(int userId)
        {
            return UserQueries.GetUserById(userId);
        }

        static public User GetUserByUsername(string username)
        {
            return UserQueries.GetUserByUsername(username);
        }

        static public int CheckCredentials(string username, string password)
        {
            if (password.Length == 0 || username.Length == 0)
            {
                return 0;
            }

            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
            string hashedPassword = password;

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

        static public bool EmailExists(string email)
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
