using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InnerUser
    {
        // TODO: add categories and expenses
        public int UserId { get; set; }
    }

    public class UserManager
    {
        private static UserManager _instance;
        public InnerUser CurrentUser { get; set; }

        private UserManager() { }

        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserManager();
                }
                return _instance;
            }
            set { _instance = value; }
        }
        public void LogInUser(InnerUser user)
        {
            // Set the current user when logging in
            CurrentUser = user;
        }

        public void LogOutUser()
        {
            // Reset the current user when logging out
            CurrentUser = null;
        }
    }
}
