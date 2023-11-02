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
        }
    }
}
