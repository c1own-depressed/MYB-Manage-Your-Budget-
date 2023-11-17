using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using System.Windows.Controls;
using Presentation;


namespace UnitTest
{
    [TestClass]
    public class testLogin
    {
        [TestMethod]
        public void TestLoginButtonClick_Empty()
        {
            var loginPage = new Login();

            loginPage.Button_Click(null, null);

            Assert.IsTrue(loginPage.MessageBoxShown);
        }

        [TestMethod]
        public void TestLoginButtonClick_Correct()
        {
            var loginPage = new Login();
            string validUsername = "123";
            string validPassword = "123";

            loginPage.UsernameTextBox.Text = validUsername;
            loginPage.PasswordTextBox.Text = validPassword;
            loginPage.Button_Click(null, null);

            Assert.IsTrue(UserManager.Instance.CurrentUser != null);
        }
    }
}
