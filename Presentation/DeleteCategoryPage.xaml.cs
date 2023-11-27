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
    /// Interaction logic for DeleteCategoryPage.xaml
    /// </summary>
    public partial class DeleteCategoryPage : Window
    {
        private int categoryId;

        public DeleteCategoryPage(object main, int categoryId)
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
            this.categoryId = categoryId;
            int userId=UserManager.CurrentUser.Id;
            ExpenseCategoryWithExpenses? temp=MainPageLogic.GetCategoriesAndExpensesByUserId(userId).FirstOrDefault(x=>x.ExpenseCategory.Id==this.categoryId);
            this.DeleteCategoryPageTextBlock.Text += temp.ExpenseCategory.CategoryName + "?";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPageLogic.DeleteExpenseCategory(this.categoryId);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
