namespace Presentation
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using BLL;
    using log4net;
    using MYB_NEW;

    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Signup));

        public Signup()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Window.GetWindow(this).Content = login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = this.UsernameTextBox.Text;
            string email = this.EmailTextBox.Text;
            string password = this.PasswordTextBox.Password;

            if (LoginSignupLogic.EmailExists(email))
            {
                MessageBox.Show("This email already exists. Please choose another one!");
            }
            else
            {
                try
                {
                    LoginSignupLogic.AddUser(username, email, password);
                    MessageBox.Show("Congratulations, your are successfully registered!");
                    //Log.Error($"User with username {username} registered successfully");
                    Logger.WriteLog($"User with username {username} registered successfully", LogLevel.Info);

                    Login main = new Login();
                    Window.GetWindow(this).Content = main;
                }
                catch (Exception ex)
                {
                    //Log.Error("Registration failed", ex);
                    //Logger.WriteLog($"Registration failed for user with username: {username}, email: {email}, password: {password} registered successfully", LogLevel.Error);
                    Logger.WriteLog($"{ex.Message}", LogLevel.Error);
                }
            }
        }
    }
}
