namespace OtherPages
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using BLL;
    using Microsoft.Win32;
    using MYB_NEW;

    /// <summary>
    /// Interaction logic for ExportDataPage.xaml
    /// </summary>
    public partial class ExportDataPage : Page
    {
        private int userId;
        List<ExpenseCategoryWithExpenses> categoriesWithExpenses;

        public ExportDataPage()
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
            this.userId = UserManager.CurrentUser.Id;
            this.categoriesWithExpenses = NewTransactionLogic.GetCategoriesAndExpensesByUserId(this.userId);
            foreach (var categoryWithExpenses in this.categoriesWithExpenses)
            {
                this.CategoryComboBox.Items.Add(categoryWithExpenses.ExpenseCategory.CategoryName);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var category = this.CategoryComboBox.SelectedItem;
            string selectedCategory = category?.ToString() ?? "Unknown";
            if (string.IsNullOrEmpty(selectedCategory))
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            DateTime? from = this.StartDatePicker.SelectedDate;
            DateTime? to = this.EndDatePicker.SelectedDate;
            if (!from.HasValue || !to.HasValue)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            try
            {
                var expensesAndCategory = NewTransactionLogic.GetCategoriesAndExpensesByUserId(this.userId)
                    .FirstOrDefault(category => category.ExpenseCategory.CategoryName == selectedCategory);
                MemoryStream memoryStream = ExportDataLogic.GetCSVMemoryStream(expensesAndCategory.ExpenseCategory.Id, from.Value, to.Value);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string fileName = saveFileDialog.FileName;
                    File.WriteAllBytes(fileName, memoryStream.ToArray());
                    MessageBox.Show("File saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}