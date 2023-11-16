// <copyright file="UserQueries.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
// <summary>
// File: UserQueries.cs
// Provides methods for querying and manipulating user data.
// </summary>
// <author>Your Name</author>
//-----------------------------------------------------------------------
namespace DAL
{
    using MYB.DAL;

    /// <summary>
    /// Provides methods for querying and manipulating user data.
    /// </summary>
    public static class UserQueries
    {
        private static readonly AppDBContext Context;

        /// <summary>
        /// Initializes static members of the <see cref="UserQueries"/> class.
        /// Initializes a new instance of the <see cref="UserQueries"/> class.
        /// </summary>
        static UserQueries()
        {
            Context = new AppDBContext();
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        public static User GetUserById(int userId)
        {
            List<User> users = (from user in Context.Users
                                where user.Id == userId
                                select user).ToList();
            if (users.Count == 1)
            {
                return users[0];
            }

            throw new Exception("User is not found");
        }

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username.</returns>
        public static User GetUserByUsername(string username)
        {
            List<User> users = (from user in Context.Users
                                where user.Username == username
                                select user).ToList();
            if (users.Count == 1)
            {
                return users[0];
            }

            throw new Exception("User is not found");
        }

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user with the specified email, or null if not found.</returns>
        public static User? GetUserByEmail(string email)
        {
            List<User> users = (from user in Context.Users
                                where user.Email == email
                                select user).ToList();
            if (users.Count == 1)
            {
                return users[0];
            }

            // throw new Exception("User is not found");
            return null;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public static void AddUser(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        /// <summary>
        /// Updates a user's information.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="language">The new language for the user.</param>
        /// <param name="isLightTheme">Whether the user prefers a light theme.</param>
        /// <param name="currency">The new currency for the user.</param>
        /// <returns>The updated user.</returns>
        public static User UpdateUser(int userId, string language, bool isLightTheme, string currency)
        {
            var existingUser = Context.Users.Find(userId);

            if (existingUser != null)
            {
                existingUser.LightTheme = isLightTheme;
                existingUser.Language = language;
                existingUser.Currency = currency;

                Context.SaveChanges();
            }

            return GetUserById(userId);
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public static void DeleteUser(int userId)
        {
            var userToDelete = Context.Users.Find(userId);

            if (userToDelete != null)
            {
                Context.Users.Remove(userToDelete);
                Context.SaveChanges();
            }
        }
    }
}
