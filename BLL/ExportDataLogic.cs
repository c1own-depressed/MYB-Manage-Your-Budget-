namespace BLL
{
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ExportDataLogic
    {
        static public string GetExportDataByUserId(int expenseCategoryId, DateTime from, DateTime to)
        {
            string exportData = "";
            List<Expense> expenses = DAL.ExpenseQueries.GetExpensesByExpenseCategoryId(expenseCategoryId);
            ExpenseCategory expenseCategory = DAL.ExpenseCategoryQueries.GetExpenseCategoryById(expenseCategoryId);
            foreach (var expense in expenses)
            {
                List<Transaction> transactions = DAL.TransactionQueries.GetTransactionsByExpenseId(expense.Id);

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

        static public MemoryStream GetCSVMemoryStream(int expenseCategoryId, DateTime from, DateTime to)
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
