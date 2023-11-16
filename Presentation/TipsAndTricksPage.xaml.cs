namespace Presentation
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using MYB_NEW;

    /// <summary>
    /// Interaction logic for TipsAndTricksPage.xaml.
    /// </summary>
    public partial class TipsAndTricksPage : Page
    {
        public TipsAndTricksPage()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}
