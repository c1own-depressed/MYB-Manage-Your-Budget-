using BLL;
using MySqlConnector;


namespace ConsoleAppUnitTest
{
    [TestClass]
    public class ConsoleAppUnitTests
    {
        [TestMethod]
        public void AddUser_ShouldAddUserToDatabase()
        {
            // Arrange
            string username = "testUser";
            string email = "test@example.com";
            string password = "password123";

            // Act
            LoginSignupLogic.AddUser(username, email, password);

            // Assert
            DAL.User retrievedUser = GetUserByUsername(username);
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(username, retrievedUser.Username);
            Assert.AreEqual(email, retrievedUser.Email);
            Assert.AreEqual(password, retrievedUser.HashedPassword);
        }

        [TestMethod]
        public void FillDatabaseWithTestData_ShouldAddUsersToDatabase()
        {
            // Arrange & Act
            MYB.Program.FillDatabaseWithTestData();

            // Assert
            for (int i = 0; i < 50; i += 20)
            {
                DAL.User retrievedUser = GetUserByUsername($"username{i}");
                Assert.IsNotNull(retrievedUser);
                Assert.AreEqual($"username{i}", retrievedUser.Username);
                Assert.AreEqual($"user{i}@gmail.com", retrievedUser.Email);
            }
        }

        private DAL.User GetUserByUsername(string username)
        {
            using (var connection = new MySqlConnection(MYB.Program.ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM User WHERE username = @username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAL.User(Convert.ToString(reader["username"]), Convert.ToString(reader["email"]), Convert.ToString(reader["password"]), true, "ua", "uah");
                        }
                        return null;
                    }
                }
            }
        }
    }
}
