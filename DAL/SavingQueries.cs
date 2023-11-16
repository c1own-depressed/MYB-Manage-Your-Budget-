using Microsoft.EntityFrameworkCore;
using MYB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class SavingQueries
    {
        private static readonly AppDBContext _context;

        static SavingQueries()
        {
            _context = new AppDBContext();
        }

        public static List<Saving> GetSavingsByUserId(int userID)
        {
            return (from saving in _context.Savings
                    where saving.UserId == userID
                    select saving).ToList();
        }

        public static void AddSaving(Saving saving)
        {
            _context.Savings.Add(saving);
            _context.SaveChanges();
        }
    }
}
