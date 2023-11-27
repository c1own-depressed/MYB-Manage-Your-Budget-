namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DAL;

    public class ExportDataLogic
    {
        public static string GetExportDataByUserId(int expenseCategoryId, DateTime from, DateTime to)
        {
            string exportData = string.Empty;
            List<Expense> expenses = DAL.ExpenseQueries.GetExpensesByExpenseCategoryId(expenseCategoryId);
            ExpenseCategory expenseCategory = DAL.ExpenseCategoryQueries.GetExpenseCategoryById(expenseCategoryId);
            foreach (var expense in expenses)
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month; 
                List<Transaction> transactions = DAL.TransactionQueries.GetTransactionsByExpenseIdYearAndMonth(expense.Id, year, month);

                foreach (var transaction in transactions)
                {
                    if (from <= transaction.Date && transaction.Date <= to)
                    {
                        exportData += expenseCategory.CategoryName + "," + expense.ExpenseName + "," + transaction.Amount.ToString() + "," + transaction.Date.ToString() + "\n";
                    }
                }
            }

            return exportData;
        }

        public static MemoryStream GetCSVMemoryStream(int expenseCategoryId, DateTime from, DateTime to)
        {
            string data = GetExportDataByUserId(expenseCategoryId, from, to);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    writer.WriteLine(data);
                    memoryStream.Position = 0;

                    return memoryStream;
                }
            }
        }
    }
}
