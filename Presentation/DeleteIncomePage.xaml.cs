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
    /// Interaction logic for DeleteIncomePage.xaml
    /// </summary>
    public partial class DeleteIncomePage : Window
    {
        private int incomeId;

        public DeleteIncomePage(object main,int incomeId)
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
            this.incomeId = incomeId;
            int userId = UserManager.CurrentUser.Id;
            DAL.Income? temp = MainPageLogic.GetIncomesByUserId(userId).FirstOrDefault(x => x.Id == this.incomeId);
            this.DeleteIncomePageTextBlock.Text += temp.IncomeName + "?";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainPageLogic.DeleteIncome(this.incomeId);
            this.Close();
        }
    }
}
