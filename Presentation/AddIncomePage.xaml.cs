using BLL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MYB_NEW
{
    public partial class AddIncomePage : Window
    {
        private StackPanel incomeListView; // Додайте поле для посилання на IncomeListView

        public AddIncomePage(StackPanel listView) // Змініть конструктор, щоб очікувати StackPanel
        {
            InitializeComponent();
            this.incomeListView = listView; // Збережіть посилання на IncomeListView
        }

        // Додайте обробник подій для кнопки "Add" на сторінці "AddIncomePage"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримайте дані з текстових полів
            string incomeTitle = IncomeTitleTextBox.Text;
            double projectedIncome = double.Parse(ProjectedIncomeTextBox.Text); // Попередньо перевірте правильність введення
            
            InnerUser currentUser = UserManager.Instance.CurrentUser;
            int userId = currentUser.UserId;
            IncomeQueries.AddIncome(userId, incomeTitle, (int)projectedIncome);

            // Створіть новий об'єкт доходу
            IncomeUI newIncome = new IncomeUI(incomeTitle, projectedIncome);

            // Додайте новий дохід до списку доходів на головній сторінці (IncomeListView)
            if (incomeListView != null)
            {
                incomeListView.Children.Add(new TextBlock() { Text = newIncome.Title, FontSize = 40, FontWeight = FontWeights.DemiBold });
            }

            // Закрийте сторінку "AddIncomePage"
            this.Close();
        }
    }
}
