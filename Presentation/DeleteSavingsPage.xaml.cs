using BLL;
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
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for DeleteSavingsPage.xaml
    /// </summary>
    public partial class DeleteSavingsPage : Window
    {
        private int savingId;

        public DeleteSavingsPage(object main, int savingId)
        {
            if (UserManager.CurrentUser.Language == "ua")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            InitializeComponent();
            this.savingId = savingId;
            int userId = UserManager.CurrentUser.Id;
            DAL.Saving? temp = MainPageLogic.GetSavingsByUserId(userId).FirstOrDefault(x => x.Id == this.savingId);
            this.DeleteSavingsPageTextBox.Text += temp.SavingName + "?";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainPageLogic.DeleteSaving(this.savingId);
            this.Close();
        }
    }
}
