namespace Presentation
{
    using System.Windows;
    using System.Windows.Controls;
    using BLL;

    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        public Signup()
        {
            this.InitializeComponent();
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
                LoginSignupLogic.AddUser(username, email, password);
                MessageBox.Show("Congratulations, user registered successfully!");

                Login main = new Login();
                Window.GetWindow(this).Content = main;
            }
        }
    }
}
