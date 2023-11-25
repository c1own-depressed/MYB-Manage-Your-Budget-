﻿namespace MYB_NEW
{
    using BLL;
    using DAL;
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
            int userId = UserManager.CurrentUser.Id;
            this.InitializeComponent();
            this.savingId = savingId;
            this.savingList = MainPageLogic.GetSavingsByUserId(userId);
            Saving? temp = this.savingList.FirstOrDefault(saving => saving.Id == this.savingId);
            this.TitleOfSavingsTextBox.Text = temp.SavingName;
            this.AmmountOfSavingsTextBox.Text = temp.Amount.ToString();
        }
    }
}
