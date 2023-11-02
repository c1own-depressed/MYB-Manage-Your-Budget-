using BLL;
using MYB.DAL;
using MYB_NEW;
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
            using (var context = new AppDBContext())
            {
                string username = UsernameTextBox.Text;
                string email = EmailTextBox.Text;
                string password = PasswordTextBox.Text;

                if (UserQueries.EmailExists(email))
                {
                    MessageBox.Show("This email already exists. Please choose another one!");
                }
                else
                {
                    UserQueries.AddUser(username, email, password);
                    MessageBox.Show("Congratulations, user registered successfully!");

                    Login main = new Login();
                    Window.GetWindow(this).Content = main;
                }
            }
        }
    }
}
