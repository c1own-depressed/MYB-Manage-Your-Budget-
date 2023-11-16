﻿using Microsoft.EntityFrameworkCore;
using MYB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class IncomeQueries
    {
        private static readonly AppDBContext _context;

        static IncomeQueries()
        {
            _context = new AppDBContext();
        }

        public static List<Income> GetIncomeByUserId(int userID)
        {
            return (from income in _context.Incomes
                    where income.UserId == userID
                    select income).ToList();
        }

        public static void AddIncome(Income income)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }
    }
}