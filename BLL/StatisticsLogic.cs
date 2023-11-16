namespace BLL
{
    public class StatisticsLogic
    {
        public class DataRecord
        {
            public string Date { get; set; }

            public int Income { get; set; }

            public int SummaryExpenses { get; set; }

            public int Saved { get; set; }
        }

        //static public List<Transaction> GetAllTransactions()
        //{
        //    List<Transaction> allTransactions = new List<Transaction>();
        //    foreach (var expenseCategoryWithExpense in UserManager.userExpenseCategoriesWithExpenses)
        //    {
        //        foreach (var expense in expenseCategoryWithExpense.expenses)
        //        {
        //            var transactions = TransactionQueries.GetTransactionsByExpenseId(expense.Id);
        //            foreach (var transaction in transactions)
        //            {
        //                allTransactions.Add(transaction);
        //            }
        //        }
        //    }

        //    return allTransactions;
        //}

        //static public List<DataRecord> GetRecordsFromDatabase()
        //{
        //    var allTransactions = GetAllTransactions();

        //    List<DataRecord> records = new List<DataRecord>();
        //    foreach (var transaction in allTransactions)
        //    {
        //        records.Add(new DataRecord { 

        //        });
        //    }
        //}
    }
}
