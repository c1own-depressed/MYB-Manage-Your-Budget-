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
            InitializeComponent();
            userId = UserManager.CurrentUser.Id;
            categoriesWithExpenses = NewTransactionLogic.GetCategoriesAndExpensesByUserId(userId);
            foreach (var categoryWithExpenses in categoriesWithExpenses)
            {
                CategoryComboBox.Items.Add(categoryWithExpenses.ExpenseCategory.CategoryName);
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem category = (ComboBoxItem)CategoryComboBox.SelectedItem;
            string selectedCategory = (category?.Content?.ToString() ?? "Unknown");


            if (string.IsNullOrEmpty(selectedCategory))
            {
                MessageBox.Show("Please select a category.");
                return;
            }


            DateTime? from = StartDatePicker.SelectedDate;
            DateTime? to = EndDatePicker.SelectedDate;


            if (!from.HasValue || !to.HasValue)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            try
            {
           
                var expensesAndCategory = NewTransactionLogic.GetCategoriesAndExpensesByUserId(userId)
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

        //private void ExportButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string category = CategoryComboBox.SelectedItem?.ToString();
        //    string type = DataTypeComboBox.SelectedItem?.ToString();
        //    DateTime? startDate = StartDatePicker.SelectedDate;
        //    DateTime? endDate = EndDatePicker.SelectedDate;
        //    Console.Write(startDate);
        //    if (category == null || type == null || !startDate.HasValue || !endDate.HasValue)
        //    {
        //        MessageBox.Show("Please fill in all fields.");
        //        return;
        //    }

        //    // Виконайте SQL-запит для отримання даних за певний період та з обраними параметрами (category, type, startDate, endDate)
        //    string connectionString = "server=127.0.0.1;uid=root;pwd=1234;database=mybdb";
        //    string sqlQuery = "SELECT * FROM User LEFT JOIN Income ON User.id = Income.user_id JOIN Saving ON User.id = Saving.user_id LEFT JOIN ExpenseCategory ON User.id = ExpenseCategory.user_id LEFT JOIN Expense ON ExpenseCategory.id = Expense.expense_category_id LEFT JOIN Transaction ON Expense.id = Transaction.expense_id WHERE User.id BETWEEN 1 AND 50 and ExpenseCategory.category_name = @category AND User.language = 'ua' AND User.currency = 'uan' AND Transaction.date BETWEEN @startDate AND @endDate; ";

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@category", category);
        //            command.Parameters.AddWithValue("@startDate", startDate);
        //            command.Parameters.AddWithValue("@endDate", endDate);

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                SaveFileDialog saveFileDialog = new SaveFileDialog
        //                {
        //                    Filter = "Excel Files|*.xlsx",
        //                    Title = "Save Data to Excel File"
        //                };

        //                if (saveFileDialog.ShowDialog() == true)
        //                {
        //                    string filePath = saveFileDialog.FileName;
        //                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //                    var package = new ExcelPackage(new FileInfo(filePath));
        //                    var worksheet = package.Workbook.Worksheets.Add("Data");
        //                    int row = 1;

        //                    // Додавання заголовків
        //                    worksheet.Cells[row, 1].Value = "Username";
        //                    worksheet.Cells[row, 2].Value = "Transaction Name";
        //                    worksheet.Cells[row, 3].Value = "Category Name";
        //                    worksheet.Cells[row, 4].Value = "Date";
        //                    worksheet.Cells[row, 5].Value = "Amount";
        //                    row++;

        //                    while (reader.Read())
        //                    {
        //                        worksheet.Cells[row, 1].Value = reader["username"];
        //                        worksheet.Cells[row, 2].Value = reader["transaction_name"];
        //                        worksheet.Cells[row, 3].Value = reader["category_name"];
        //                        worksheet.Cells[row, 4].Value = reader["date"];
        //                        worksheet.Cells[row, 5].Value = reader["amount"];
        //                        row++;
        //                    }

        //                    package.Save();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}