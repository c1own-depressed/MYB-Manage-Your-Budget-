using BLL;
using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTest.BLLTests
{
    //public class LoginSignup
    //{
    //    [Fact]
    //    public void TestLoginButtonClick_Empty()
    //    {
    //        var loginPage = new Login();

    //        loginPage.Button_Click(null, null);

    //        Assert.True(loginPage.MessageBoxShown);
    //    }

    //    [Fact]
    //    public void TestLoginButtonClick_Correct()
    //    {
    //        var loginPage = new Login();
    //        string validUsername = "123";
    //        string validPassword = "123";

    //        loginPage.UsernameTextBox.Text = validUsername;
    //        loginPage.PasswordTextBox.Text = validPassword;
    //        loginPage.Button_Click(null, null);

    //        Assert.NotNull(UserManager.Instance.CurrentUser);
    //    }
    //}
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void LoadDataCommand_LoadsData()
        {
            // Arrange
            var dataServiceMock = new Mock<IDataService>();
            var viewModel = new MainViewModel(dataServiceMock.Object);

            // Act
            viewModel.LoadDataCommand.Execute(null);

            // Assert
            Assert.IsNotNull(viewModel.Data);
            // Add more assertions based on your expected behavior
        }
    }
}
