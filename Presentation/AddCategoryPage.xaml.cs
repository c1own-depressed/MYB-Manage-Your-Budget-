using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MYB_NEW
{
    public partial class AddCategoryPage : Window
    {
        private Main mainPage;
        private Dictionary<Button, StackPanel> categoryExpenseButtonMap = new Dictionary<Button, StackPanel>();

        public AddCategoryPage(Main mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryTitle = CategoryTitleTextBox.Text;

            InnerUser currentUser = UserManager.Instance.CurrentUser;
            int userId = currentUser.UserId;

            MainPageLogic.AddExpenseCategory(userId, categoryTitle);


            // Create a new category block
            Border newCategoryBlock = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0),
                Width = 360,
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            StackPanel newCategoryStackPanel = new StackPanel();

            TextBlock categoryHeader = new TextBlock
            {
                Text = categoryTitle,
                FontSize = 48,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 100,
            };

            StackPanel categoryListView = new StackPanel();

            Button addCategoryExpenseButton = new Button
            {
                Content = "Add Expense",
                Style = (Style)mainPage.Resources["InvisibleButtonStyle"],
                Width = 360,
                FontWeight = FontWeights.Bold,
                FontSize = 35,
            };

            addCategoryExpenseButton.Click += AddCategoryExpenseButton_Click;
            categoryExpenseButtonMap.Add(addCategoryExpenseButton, categoryListView);

            categoryListView.Children.Add(addCategoryExpenseButton);

            newCategoryStackPanel.Children.Add(categoryHeader);
            newCategoryStackPanel.Children.Add(categoryListView);
            newCategoryBlock.Child = newCategoryStackPanel;

            // Find the last category and insert the new one below it
            var lastCategory = mainPage.CategoriesListView.Children.OfType<Border>().LastOrDefault();
            if (lastCategory != null)
            {
                int insertIndex = mainPage.CategoriesListView.Children.IndexOf(lastCategory) + 1;
                mainPage.CategoriesListView.Children.Insert(insertIndex, newCategoryBlock);
            }
            else
            {
                mainPage.CategoriesListView.Children.Add(newCategoryBlock);
            }

            this.Close();
        }

        private void AddCategoryExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (categoryExpenseButtonMap.ContainsKey(clickedButton))
            {
                StackPanel expenseCategory = categoryExpenseButtonMap[clickedButton];
                AddExpensePage addExpensePage = new AddExpensePage(expenseCategory);
                addExpensePage.ShowDialog();
            }
        }
    }
}
