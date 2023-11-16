using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class UserManager
    {
        public static User CurrentUser { get; set; }
        public static List<ExpenseCategoryWithExpenses> userExpenseCategoriesWithExpenses { get; set; }   

        public static void LogInUser(int userId)
        {
            // Set the current user when logging in
            CurrentUser = LoginSignupLogic.GetUser(userId);
            userExpenseCategoriesWithExpenses = NewTransactionLogic.GetCategoriesAndExpensesByUserId(userId);
        }

        public static void LogOutUser()
        {
            // Reset the current user when logging out
            CurrentUser = null;
        }
    }
}
