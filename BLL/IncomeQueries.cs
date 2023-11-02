using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IncomeQueries
    {
        //static public Income AddIncome(string username, string email, string password)
        //{
        //    return new Income();
        //}

        static public List<Income> GetIncomesByUserId(int userId)
        {
            List<Income> incomes = Queries.GetIncomeByUserId(userId);
            return incomes;
        }
    }
}
