using MYB.DAL;


namespace DAL
{
    public static class UserQueries
    {
        private static readonly AppDBContext _context;

        static UserQueries()
        {
            _context = new AppDBContext();
        }

        public static User GetUserById(int userId)
        {
            List<User> users = (from user in _context.Users
                                where user.Id == userId
                                select user).ToList();
            if (users.Count == 1)
            {
                return users[0];
            }
            throw new Exception("User is not found");
        }

        public static User GetUserByUsername(string username)
        {
            List<User> users = (from user in _context.Users
                                where user.Username == username
                                select user).ToList();
            if (users.Count == 1)
            {
                return users[0];
            }
            throw new Exception("User is not found");
        }

        public static User? GetUserByEmail(string email)
        {
            List<User> users = (from user in _context.Users
                                where user.Email == email
                                select user).ToList();
            if (users.Count == 1)
            {
                return users[0];
            }
            //throw new Exception("User is not found");
            return null;
        }

        public static void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public static User UpdateUser(int userId, string language, bool isLightTheme, string currency)
        {
            var existingUser = _context.Users.Find(userId);

            if (existingUser != null)
            {
                existingUser.LightTheme = isLightTheme;
                existingUser.Language = language;
                existingUser.Currency = currency;

                _context.SaveChanges();
            }

            return GetUserById(userId);
        }

        public static void DeleteUser(int userId)
        {
            var userToDelete = _context.Users.Find(userId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
}
