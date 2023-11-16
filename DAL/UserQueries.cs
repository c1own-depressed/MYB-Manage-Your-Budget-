using Microsoft.EntityFrameworkCore;
using MYB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //return _context.Users.FirstOrDefault(u => u.Id == userId);
            return (from user in _context.Users
                    where user.Id == userId
                    select user).FirstOrDefault();
        }

        public static User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
            //return (from user in _context.Users
            //        where user.Username == username
            //        select user).FirstOrDefault();
        }

        public static User GetUserByEmail(string email)
        {
            return (from user in _context.Users
                    where user.Email == email
                    select user).FirstOrDefault();
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
