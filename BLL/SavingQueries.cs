using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SavingQueries
    {
        static public List<Saving> GetSavingsByUserId(int userId)
        {
            List<Saving> savings = Queries.GetSavingsByUserId(userId);
            return savings;
        }
    }
}
