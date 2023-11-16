using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SettingsLogic
    {
        static public User GetUser(int userId)
        {
            return DAL.UserQueries.GetUserById(userId);
        }

        static public User UpdateUser(int userId, string language, bool isLightTheme, string currency)
        {
            return DAL.UserQueries.UpdateUser(userId, language, isLightTheme, currency);
        }
    }
}
