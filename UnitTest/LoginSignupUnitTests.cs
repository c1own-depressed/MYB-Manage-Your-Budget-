using BLL;


namespace LoginSignupUnitTest
{
    [TestClass]
    public class LoginSignupLogicTests
    {
        // TODO: check hash

        [TestMethod]
        public void AddUser_ShouldAddUserToDatabase()
        {
            string username = "test301";
            string email = "test301@gmail.com";
            string password = "password123";

            DAL.User addedUser = LoginSignupLogic.AddUser(username, email, password);

            DAL.User retrievedUser = DAL.UserQueries.GetUserById(addedUser.Id);
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(username, retrievedUser.Username);
            Assert.AreEqual(email, retrievedUser.Email);
            Assert.AreEqual(LoginSignupLogic.HashString(password), retrievedUser.HashedPassword);
        }

        [TestMethod]
        public void UpdateUser_ShouldUpdateUserInDatabase()
        {
            string username = "test301";
            string language = "en";
            bool isLightTheme = true;
            string currency = "usd";

            DAL.User user = DAL.UserQueries.GetUserByUsername(username);    

            DAL.User updatedUser = LoginSignupLogic.UpdateUser(user.Id, language, isLightTheme, currency);

            DAL.User retrievedUser = DAL.UserQueries.GetUserById(user.Id);
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(language, retrievedUser.Language);
            Assert.AreEqual(isLightTheme, retrievedUser.LightTheme);
            Assert.AreEqual(currency, retrievedUser.Currency);
        }

        [TestMethod]
        public void GetUser_ShouldRetrieveUserFromDatabase()
        {
            int userId = 1;

            DAL.User retrievedUser = LoginSignupLogic.GetUser(userId);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(userId, retrievedUser.Id);
        }

        [TestMethod]
        public void GetUserByUsername_ShouldRetrieveUserFromDatabase()
        {
            string username = "test301";

            DAL.User retrievedUser = LoginSignupLogic.GetUserByUsername(username);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(username, retrievedUser.Username);
        }

        [TestMethod]
        public void CheckCredentials_WithValidCredentials_ShouldReturnUserId()
        {
            string username = "test302";
            string email = "test302@gmail.com";
            string password = "test302";
            string hashedPassword = LoginSignupLogic.HashString(password);

            DAL.User user = new DAL.User(username, email, hashedPassword, true, "en", "uah");

            DAL.UserQueries.AddUser(user);

            int result = LoginSignupLogic.CheckCredentials(username, password);

            Assert.AreEqual(user.Id, result);
        }

        [TestMethod]
        public void CheckCredentials_WithInvalidCredentials_ShouldReturnZero()
        {
            string username = "test303";
            string email = "test303@gmail.com";
            string password = "test303";
            string hashedPassword = LoginSignupLogic.HashString(password);

            DAL.User user = new DAL.User(username, email, hashedPassword, true, "en", "uah");

            DAL.UserQueries.AddUser(user);

            int result = LoginSignupLogic.CheckCredentials(username, "test");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void EmailExists_WithExistingEmail_ShouldReturnTrue()
        {
            string username = "test304";
            string email = "test304@gmail.com";
            string password = "test304";
            string hashedPassword = LoginSignupLogic.HashString(password);

            DAL.User user = new DAL.User(username, email, hashedPassword, true, "en", "uah");
            
            DAL.UserQueries.AddUser(user);
            
            bool result = LoginSignupLogic.EmailExists(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EmailExists_WithNonExistingEmail_ShouldReturnFalse()
        {
            string email = "nonexistent@example.com";

            bool result = LoginSignupLogic.EmailExists(email);

            Assert.IsFalse(result);
        }
    }
}
