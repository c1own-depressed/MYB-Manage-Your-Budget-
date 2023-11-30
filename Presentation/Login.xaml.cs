namespace Presentation
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using BLL;
    using MYB_NEW;

    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //GuestPage guestPage = new GuestPage();
            Signup signup = new Signup();
            Window.GetWindow(this).Content = signup;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = this.UsernameTextBox.Text;
            string password = this.PasswordTextBox.Password;
            // bool rememberMe = checkBox.IsChecked == true;

            if (username.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Login or password is empty");
            }
            else
            {
                int userId = LoginSignupLogic.CheckCredentials(username, password);
                if (Convert.ToBoolean(userId))
                {
                    UserManager.LogInUser(userId);

                    Main main = new Main();
                    Window.GetWindow(this).Content = main;
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again!");
                }
            }
        }
    }
}
