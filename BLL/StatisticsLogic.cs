using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //    foreach (var expenseCategoryWithExpense in UserManager.userExpenseCategoriesWithExpenses)
        //    {
        //        foreach (var expense in expenseCategoryWithExpense.expenses)
        //        {
                    
        //        }
        //    }
        //}

        //static public List<DataRecord> GetRecordsFromDatabase()
        //{

        //}
    }
}
