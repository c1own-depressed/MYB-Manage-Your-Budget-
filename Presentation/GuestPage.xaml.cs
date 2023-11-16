namespace Presentation
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for GuestPage.xaml.
    /// </summary>
    public partial class GuestPage : Page
    {
        public GuestPage()
        {
            this.InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mainFrame.Navigate(new Login());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Login addIncomePage2 = new Login();

            // addIncomePage2.ShowDialog();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
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
