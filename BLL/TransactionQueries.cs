using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    internal class TransactionQueries
    {
        static public Transaction AddTransaction(string transactionName, int amount, int expenseId)
        {
            Transaction transaction = new Transaction
            {
                TransactionName = transactionName,
                Amount = amount,
                Date = DateTime.Now,
                ExpenseId = expenseId
            };

            Queries.AddTransaction(transaction);

            return transaction;
        }

        static public string GetExportDataByUserId(int expenseCategoryId, DateTime from, DateTime to)
        {
            string exportData = "";
            List<Expense> expenses = Queries.GetExpensesByExpenseCategoryId(expenseCategoryId);
            ExpenseCategory expenseCategory = Queries.GetExpenseCategoryById(expenseCategoryId);
            foreach (var expense in expenses)
            {
                List<Transaction> transactions = Queries.GetTransactionsByExpenseId(expense.Id);

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
