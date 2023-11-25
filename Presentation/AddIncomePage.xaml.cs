namespace MYB_NEW
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using BLL;

    public partial class AddIncomePage : Window
    {
        private StackPanel incomeListView;

        public AddIncomePage(StackPanel listView)
        {
            this.InitializeComponent();
            this.incomeListView = listView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string incomeTitle = this.IncomeTitleTextBox.Text;
            double projectedIncome = double.Parse(this.ProjectedIncomeTextBox.Text); // Попередньо перевірте правильність введення
            int userId = UserManager.CurrentUser.Id;
            MainPageLogic.AddIncome(userId, incomeTitle, (int)projectedIncome);
            var temp = MainPageLogic.GetIncomesByUserId(userId).FirstOrDefault(x => x.IncomeName == incomeTitle);
            int id = temp.Id;
            // Створіть новий об'єкт доходу
            IncomeUI newIncome = new IncomeUI(incomeTitle, projectedIncome);

            // Створіть кнопки "Edit" та "Delete" для нового доходу
            Button editButton = new Button
            {
                Style = (Style)this.Resources["InvisibleButtonStyle"],
                Width = 41,
                Height = 41,
                Name = $"EditIncomeButton_{Guid.NewGuid():N}",
                Content = new TextBlock
                {
                    Text = "E",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                },
            };

            editButton.Click += (sender, e) => this.EditIncome_Click(sender, e, id);
            Button deleteButton = new Button
            {
                Style = (Style)this.Resources["InvisibleButtonStyle"],
                Width = 41,
                Height = 41,
                Name = $"DeleteIncomeButton_{Guid.NewGuid().ToString("N")}",
                Content = new TextBlock
                {
                    Text = "D",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                },
            };

            deleteButton.Click += this.DeleteIncome_Click;


            // Додайте новий дохід разом із кнопками "Edit" та "Delete" до списку доходів на головній сторінці
            if (this.incomeListView != null)
            {
                StackPanel incomePanel = new StackPanel();
                incomePanel.Orientation = Orientation.Horizontal;
                TextBlock incomeTextBlock = new TextBlock
                {
                    Text = newIncome.Title,
                    FontSize = 40,
                    FontWeight = FontWeights.DemiBold,
                };
                incomePanel.Children.Add(incomeTextBlock);
                incomePanel.Children.Add(editButton);
                incomePanel.Children.Add(deleteButton);
                this.incomeListView.Children.Add(incomePanel);
            }

            // Закрийте сторінку "AddIncomePage"
            this.Close();
        }

        private void EditIncome_Click(object sender, RoutedEventArgs e, int incomeId)
        {
            EditncomePage addCategoryPage = new EditncomePage(this, incomeId);
            addCategoryPage.ShowDialog();
        }

        private void DeleteIncome_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для видалення доходу
        }
    }
}