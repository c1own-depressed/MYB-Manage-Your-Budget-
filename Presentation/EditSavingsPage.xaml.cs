namespace MYB_NEW
{
    using BLL;
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for EditSavingsPage.xaml
    /// </summary>
    public partial class EditSavingsPage : Window
    {
        int savingId;
        List<Saving> savingList;

        public EditSavingsPage(object main, int savingId)
        {
            try
            {
                if (UserManager.CurrentUser.Language == "ua")
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
                }
                else
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                }

                int userId = UserManager.CurrentUser.Id;
                this.InitializeComponent();
                this.savingId = savingId;
                this.savingList = MainPageLogic.GetSavingsByUserId(userId);
                Saving? temp = this.savingList.FirstOrDefault(saving => saving.Id == this.savingId);
                this.TitleOfSavingsTextBox.Text = temp.SavingName;
                this.AmmountOfSavingsTextBox.Text = temp.Amount.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, LogLevel.Error);
            }
}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int AmountOfSaving = Convert.ToInt32(this.AmmountOfSavingsTextBox.Text);
                string TitleOfSavings = this.TitleOfSavingsTextBox.Text;
                MainPageLogic.EditSaving(this.savingId, TitleOfSavings, AmountOfSaving);
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, LogLevel.Error);
                this.TitleOfSavingsTextBox.Text = string.Empty;
                this.AmmountOfSavingsTextBox.Text = string.Empty;
                // TODO: change input field borders to red
            }
        }
    }
}
