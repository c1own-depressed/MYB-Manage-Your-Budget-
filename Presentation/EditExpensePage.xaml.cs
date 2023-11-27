namespace MYB_NEW
{
    using BLL;
    using System.Windows;

    /// <summary>
    /// Interaction logic for EditExpensePage.xaml
    /// </summary>
    public partial class EditExpensePage : Window
    {
        public EditExpensePage()
        {
            if (UserManager.CurrentUser.Language == "ua")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            this.InitializeComponent();
        }
    }
}
