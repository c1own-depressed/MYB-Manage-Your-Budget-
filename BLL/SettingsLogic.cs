namespace BLL
{
    using DAL;

    public class SettingsLogic
    {
        public static User GetUser(int userId)
        {
            return DAL.UserQueries.GetUserById(userId);
        }

        public static User UpdateUser(int userId, string language, bool isLightTheme, string currency)
        {
            return DAL.UserQueries.UpdateUser(userId, language, isLightTheme, currency);
        }
    }
}
