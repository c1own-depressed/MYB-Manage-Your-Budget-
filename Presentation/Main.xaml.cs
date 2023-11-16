using BLL;
using DAL;
using OtherPages;
using Presentation;
using reg;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace MYB_NEW
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            InnerUser currentUser = UserManager.Instance.CurrentUser;
            int userId = currentUser.UserId;

            List<Income> incomes = IncomeQueries.GetIncomesByUserId(userId);
            List<Saving> savings = SavingQueries.GetSavingsByUserId(userId);
            List<ExpenseCategoryWithExpenses> expenses = ExpenseQueries.GetCategoriesAndExpensesByUserId(userId);

            for (int i = 0; i < incomes.Count; i++)
            {
                IncomeListView.Children.Add(new TextBlock() { Text = incomes[i].IncomeName, FontSize = 40, FontWeight = FontWeights.DemiBold });
            }
            for (int i = 0; i < savings.Count; i++)
            {
                SavingsListView.Children.Add(new TextBlock() { Text = savings[i].SavingName, FontSize = 40, FontWeight = FontWeights.DemiBold });
            }

            User user = UserQueries.GetUser(userId);
            UsernameTextBlock.Text = user.Username;

            SetCategoriesList(expenses);
        }

        private void SetCategoriesList(List<ExpenseCategoryWithExpenses> expenses)
        {
            for (int i = 0; i < expenses.Count; i++)
            {
                int categoryID = expenses[i].expenseCategory.Id;
                Dictionary<Button, StackPanel> categoryExpenseButtonMap = new Dictionary<Button, StackPanel>();
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
                    Text = expenses[i].expenseCategory.CategoryName,
                    FontSize = 40,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Height = 100,
                };

                StackPanel categoryListView = new StackPanel();
                foreach (var expense in expenses[i].expenses)
                {
                    StackPanel newExpensePanel = new StackPanel();
                    newExpensePanel.Orientation = Orientation.Horizontal;

                    TextBlock newExpenseTitle = new TextBlock
                    {
                        Text = expense.ExpenseName,
                        FontSize = 20,
                        FontWeight = FontWeights.DemiBold
                    };

                    TextBlock spaceText = new TextBlock
                    {
                        Text = " ",
                        FontSize = 10,
                        FontWeight = FontWeights.DemiBold,
                        Foreground = Brushes.Gray
                    };

                    TextBlock newExpenseBudget = new TextBlock
                    {
                        Text = $"0/{expense.Amount} $",
                        FontSize = 20,
                        FontWeight = FontWeights.DemiBold,
                        Foreground = Brushes.Gray,
                        Height = 20
                    };

                    newExpensePanel.Children.Add(newExpenseTitle);
                    newExpensePanel.Children.Add(spaceText);
                    newExpensePanel.Children.Add(newExpenseBudget);

                    categoryListView.Children.Add(newExpensePanel);
                }

                Button addCategoryExpenseButton = new Button
                {
                    Content = "Add Expense",
                    Style = (Style)Resources["InvisibleButtonStyle"],
                    Width = 360,
                    FontWeight = FontWeights.Bold,
                    FontSize = 30,
                };

                addCategoryExpenseButton.Click += (sender, e) =>
                {
                    Button clickedButton = (Button)sender;

                    if (categoryExpenseButtonMap.ContainsKey(clickedButton))
                    {
                        StackPanel expenseCategory = categoryExpenseButtonMap[clickedButton];
                        
                        AddExpensePage addExpensePage = new AddExpensePage(expenseCategory, categoryID);
                        addExpensePage.ShowDialog();
                    }
                };
                categoryExpenseButtonMap.Add(addCategoryExpenseButton, categoryListView);

                categoryListView.Children.Add(addCategoryExpenseButton);

                newCategoryStackPanel.Children.Add(categoryHeader);
                newCategoryStackPanel.Children.Add(categoryListView);
                newCategoryBlock.Child = newCategoryStackPanel;

                // Find the last category and insert the new one below it
                var lastCategory = CategoriesListView.Children.OfType<Border>().LastOrDefault();
                if (lastCategory != null)
                {
                    int insertIndex = CategoriesListView.Children.IndexOf(lastCategory) + 1;
                    CategoriesListView.Children.Insert(insertIndex, newCategoryBlock);
                }
                else
                {
                    CategoriesListView.Children.Add(newCategoryBlock);
                }


                for (int j = 0; j < expenses[i].expenses.Count; j++)
                {
                    // Створіть новий об'єкт "Expense"
                    Expense newExpense = new Expense(expenses[i].expenses[j].ExpenseName, expenses[i].expenses[j].Amount, categoryListView);

                    // Додайте новий "Expense" до категорії витрат (StackPanel)
                    if (categoryListView != null)
                    {
                        StackPanel newExpensePanel = new StackPanel();
                        newExpensePanel.Orientation = Orientation.Horizontal;

                        // Назва витрати
                        TextBlock newExpenseTitle = new TextBlock
                        {
                            Text = newExpense.Title,
                            FontSize = 30,
                            FontWeight = FontWeights.DemiBold
                        };

                        // Пробіл
                        TextBlock spaceText = new TextBlock
                        {
                            Text = " ",
                            FontSize = 10,
                            FontWeight = FontWeights.DemiBold,
                            Foreground = Brushes.Gray
                        };

                        // Бюджет
                        TextBlock newExpenseBudget = new TextBlock
                        {
                            Text = $"0/{newExpense.Amount} $",
                            FontSize = 32,
                            FontWeight = FontWeights.DemiBold,
                            Foreground = Brushes.Gray,
                            Height = 32
                        };

                        // Додайте назву витрати, пробіл і бюджет в StackPanel
                        newExpensePanel.Children.Add(newExpenseTitle);
                        newExpensePanel.Children.Add(spaceText);
                        newExpensePanel.Children.Add(newExpenseBudget);

                        // Знайдіть кнопку "Add Expense" та вставте нову витрату перед нею
                        //for (int i = 0; i < expenseCategory.Children.Count; i++)
                        //{
                        //    if (expenseCategory.Children[i] is Button addExpenseButton)
                        //    {
                        //        expenseCategory.Children.Insert(i, newExpensePanel);
                        //        break;
                        //    }
                        //}
                    }
                }
            }
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            AddIncomePage addIncomePage = new AddIncomePage(IncomeListView);
            addIncomePage.ShowDialog();
        }

        private void AddSavings_Click(object sender, RoutedEventArgs e)
        {
            AddSavingsPage addSavingsPage = new AddSavingsPage(SavingsListView);
            addSavingsPage.ShowDialog();
        }


        //public void AddExpense_Click(object sender, RoutedEventArgs e)
        //{
        //    Button clickedButton = (Button)sender; // Отримайте посилання на кнопку, яка спричинила подію

        //    if (clickedButton == AddExpenseCategory1)
        //    {
        //        AddExpensePage addExpensePage = new AddExpensePage(Category1ExpensesListView);
        //        addExpensePage.ShowDialog();
        //    }
        //    else if (clickedButton == AddExpenseCategory2)
        //    {
        //        AddExpensePage addExpensePage = new AddExpensePage(Category2ExpensesListView);
        //        addExpensePage.ShowDialog();
        //    }
        //}


        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryPage addCategoryPage = new AddCategoryPage(this); // Pass 'this' as the 'mainPage' argument
            addCategoryPage.ShowDialog();
        }

        public void AddCategory(Border newCategory)
        {
            // Додайте нову категорію на сторінку "Main" над кнопкою "Add Category"
            CategoriesListView.Children.Insert(CategoriesListView.Children.Count - 1, newCategory);
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            // Тут ви можете додати функціональність для кнопки "Add Transaction"
        }
        private bool buttonsVisible = false;

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            if (buttonsVisible)
            {
                // Приховати кнопки "Statistic", "Data export", "Tips and Tricks", "Settings" і "Sign out"
                StatisticButton.Visibility = Visibility.Collapsed;
                DataExportButton.Visibility = Visibility.Collapsed;
                TipsAndTricksButton.Visibility = Visibility.Collapsed;
                SettingsButton.Visibility = Visibility.Collapsed;
                SignOutButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Показати кнопки "Statistic", "Data export", "Tips and Tricks", "Settings" і "Sign out"
                StatisticButton.Visibility = Visibility.Visible;
                DataExportButton.Visibility = Visibility.Visible;
                TipsAndTricksButton.Visibility = Visibility.Visible;
                SettingsButton.Visibility = Visibility.Visible;
                SignOutButton.Visibility = Visibility.Visible;
            }

            buttonsVisible = !buttonsVisible;
        }
        private void EditIncome_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }

        private void DeleteIncome_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void EditSavings_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void DeleteSavings_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void EditExpense_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void DeleteExpense_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            //UserManager.Instance.LogOutUser();
            Login login = new Login();
            Window.GetWindow(this).Content = login;
        }

        private void DataExportButton_Click(object sender, RoutedEventArgs e)
        {
            ExportDataPage export_data = new ExportDataPage();
            Window.GetWindow(this).Content = export_data;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsPageView settingsPage = new SettingsPageView();
            Window.GetWindow(this).Content = settingsPage;
        }

        private void TransactionButton_Click(object sender, RoutedEventArgs e)
        {
            New_transaction new_transaction = new New_transaction();
            Window.GetWindow(this).Content = new_transaction;
        }

        private void TipsAndTricksButton_Click(object sender, RoutedEventArgs e)
        {
            TipsAndTricksPage tipsAndTricksPage = new TipsAndTricksPage();
            Window.GetWindow(this).Content = tipsAndTricksPage;
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            Statistic1 statisticPage = new Statistic1();
            Window.GetWindow(this).Content = statisticPage;
        }
    }
}