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
    /// Interaction logic for GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Page
    {
        public GuestPage()
        {
            InitializeComponent();
        }

        //private void SignUpButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SignUpPage signUpPage = new SignUpPage(IncomeListView);
        //    signUpPage.ShowDialog();
        //}

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Your code here
                //SignUpPage signUpPage = new SignUpPage();
                //Window.GetWindow(this).Content = signUpPage;

                //NavigationService nav = NavigationService.GetNavigationService(this);
                //nav.Navigate(new Uri("SignUpPage.xaml", UriKind.RelativeOrAbsolute));

                //SignUpPage signUpPage = new SignUpPage();
                //Window.GetWindow(this).Content = signUpPage;

                // Navigating to a page
                mainFrame.Navigate(new Login()); // YourPage is a Page or UserControl
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Login addIncomePage2 = new Login();
            //addIncomePage2.ShowDialog();
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Create your login";
            }
        }
    }
}
