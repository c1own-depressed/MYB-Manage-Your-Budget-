using BLL;
using DAL;
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
namespace OtherPages
{
    /// <summary>
    /// Interaction logic for New_transaction.xaml
    /// </summary>
    public partial class New_transaction : Page
    {
        private int userId;
        public New_transaction()
        {
            InitializeComponent();
            InnerUser currentUser = UserManager.Instance.CurrentUser;
            userId = currentUser.UserId;

            List<ExpenseCategoryWithExpenses> categoriesWithExpenses = ExpenseQueries.GetCategoriesAndExpensesByUserId(userId);
            foreach (var categoryWithExpenses in categoriesWithExpenses)
            {
                CategoryComboBox.Items.Add(categoryWithExpenses.expenseCategory.CategoryName);
            }

          
            CategoryComboBox.SelectionChanged += CategoryComboBox_SelectionChanged;

            
            ExpenseComboBox.IsEnabled = false;
            CostTextBox.IsEnabled = false;

        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExpenseComboBox.Items.Clear();

            if (CategoryComboBox.SelectedItem != null)
            {
                
                string selectedCategory = CategoryComboBox.SelectedItem.ToString();

                
                var selectedCategoryWithExpenses = ExpenseQueries.GetCategoriesAndExpensesByUserId(userId)
                    .FirstOrDefault(category => category.expenseCategory.CategoryName == selectedCategory);

              
                if (selectedCategoryWithExpenses != null)
                {
                    foreach (var expense in selectedCategoryWithExpenses.expenses)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                        comboBoxItem.Content = expense.ExpenseName;
                        ExpenseComboBox.Items.Add(comboBoxItem);
                    }
                }

                
                ExpenseComboBox.IsEnabled = true;
                CostTextBox.IsEnabled = true;
            }
            else
            {
                CostTextBox.IsEnabled = false;
                ExpenseComboBox.IsEnabled = false;
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
 
            int cost = Convert.ToInt32(CostTextBox.Text);
            string transactionName = TransactionTextBox.Text;
            TransactionQueries.AddTransaction(transactionName, cost, 1);
        }
    }
}
