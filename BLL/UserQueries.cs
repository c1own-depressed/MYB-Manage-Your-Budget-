using DAL;
using Org.BouncyCastle.Crypto.Generators;

namespace BLL
{
    public class UserQueries
    {
        static public User AddUser(string username, string email, string password)
        {
            // Install-Package BCrypt.Net
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));

            User newUser = new User
            {
                Username = username,
                Email = email,
                HashedPassword = hashedPassword,
                LightTheme = true,
                Language = "ua",
                Currency = "uah",
            };

            Queries query = new Queries();
            query.AddUser(newUser);

            return newUser;
        }
    }
}