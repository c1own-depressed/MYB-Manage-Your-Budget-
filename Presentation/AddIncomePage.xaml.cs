using BLL;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System;

namespace MYB_NEW
{
    public partial class AddIncomePage : Window
    {
        private StackPanel incomeListView;

        public AddIncomePage(StackPanel listView)
        {
            InitializeComponent();
            this.incomeListView = listView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string incomeTitle = IncomeTitleTextBox.Text;
            double projectedIncome = double.Parse(ProjectedIncomeTextBox.Text);

            InnerUser currentUser = UserManager.Instance.CurrentUser;
            int userId = currentUser.UserId;
            IncomeQueries.AddIncome(userId, incomeTitle, (int)projectedIncome);

            // Створіть новий об'єкт доходу
            IncomeUI newIncome = new IncomeUI(incomeTitle, projectedIncome);

            // Створіть кнопки "Edit" та "Delete" для нового доходу
            Button editButton = new Button
            {
                Style = (Style)Resources["InvisibleButtonStyle"],
                Width = 41,
                Height = 41,
                Name = $"EditIncomeButton_{Guid.NewGuid().ToString("N")}",
                Content = new TextBlock
                {
                    Text = "E",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold
                }
            }; ;

            editButton.Click += EditIncome_Click;


            Button deleteButton = new Button
            {
                Style = (Style)Resources["InvisibleButtonStyle"],
                Width = 41,
                Height = 41,
                Name = $"DeleteIncomeButton_{Guid.NewGuid().ToString("N")}",
                Content = new TextBlock
                {
                    Text = "D",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold
                }
            };

            deleteButton.Click += DeleteIncome_Click;


            // Додайте новий дохід разом із кнопками "Edit" та "Delete" до списку доходів на головній сторінці
            if (incomeListView != null)
            {
                StackPanel incomePanel = new StackPanel();
                incomePanel.Orientation = Orientation.Horizontal;
                TextBlock incomeTextBlock = new TextBlock
                {
                    Text = newIncome.Title,
                    FontSize = 40,
                    FontWeight = FontWeights.DemiBold
                };
                incomePanel.Children.Add(incomeTextBlock);
                incomePanel.Children.Add(editButton);
                incomePanel.Children.Add(deleteButton);
                incomeListView.Children.Add(incomePanel);
            }

            // Закрийте сторінку "AddIncomePage"
            this.Close();
        }

        private void EditIncome_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для редагування доходу
        }

        private void DeleteIncome_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для видалення доходу
        }
    }
}