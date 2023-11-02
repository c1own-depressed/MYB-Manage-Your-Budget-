using MYB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string username = usernameTextBox.Text;
            //string email = emailTextBox.Text;
            //string password = passwordTextBox.Text;

            using (var context = new AppDBContext())
            {
                //var query = new Queries(context);

                if (true)// UserQueries.UserExists(username, email)
                {
                    MessageBox.Show("This Username or email already exists. Please choose another one!");
                }
                else
                {
                    //UserQueries.AddUser(username, email, password);

                    MessageBox.Show("Congratulations, user registered successfully!");
                    //Login logInPage = new Login();
                    //Window.GetWindow(this).Content = logInPage;

                    //Main.NavigationService.Navigate(new LogInPage());
                }
            }
        }
    }
}
