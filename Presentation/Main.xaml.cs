namespace MYB_NEW
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Xml.Linq;
    using BLL;
    using DAL;
    using OtherPages;
    using Presentation;
    using reg;

    public partial class Main : Page
    {
        List<Saving> savings;
        List<Income> incomes;
        List<ExpenseCategoryWithExpenses> expenses;

        public Main()
        {
            this.InitializeComponent();
            int userId = UserManager.CurrentUser.Id;

            this.incomes = MainPageLogic.GetIncomesByUserId(userId);
            this.savings = MainPageLogic.GetSavingsByUserId(userId);
            this.expenses = MainPageLogic.GetCategoriesAndExpensesByUserId(userId);
            this.UsernameTextBlock.Text = UserManager.CurrentUser.Username;
            this.SetIncomeList(this.incomes);
            this.SetCategoriesList(this.expenses);
            this.SetSavingsList(this.savings);
        }

        private void SetCategoriesList(List<ExpenseCategoryWithExpenses> expenses)
        {
            for (int i = 0; i < expenses.Count; i++)
            {
                int currentCategoryIndex = i;
                int categoryID = expenses[i].ExpenseCategory.Id;
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
                    Text = expenses[i].ExpenseCategory.CategoryName,
                    FontSize = 40,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Height = 100,
                };

                StackPanel categoryListView = new StackPanel();
                for (int j = 0; j < expenses[i].Expenses.Count; j++)
                {
                    int currentIndexExpense = j;
                    var expense = expenses[i].Expenses[j];

                    StackPanel newExpensePanel = new StackPanel();
                    newExpensePanel.Orientation = Orientation.Horizontal;

                    TextBlock newExpenseTitle = new TextBlock
                    {
                        Text = expense.ExpenseName,
                        FontSize = 20,
                        FontWeight = FontWeights.DemiBold,
                    };

                    TextBlock spaceText = new TextBlock
                    {
                        Text = " ",
                        FontSize = 10,
                        FontWeight = FontWeights.DemiBold,
                        Foreground = Brushes.Gray,
                    };

                    TextBlock newExpenseBudget = new TextBlock
                    {
                        Text = $"0/{expense.Amount} $",
                        FontSize = 20,
                        FontWeight = FontWeights.DemiBold,
                        Foreground = Brushes.Gray,
                        Height = 30,
                        VerticalAlignment = VerticalAlignment.Top,
                    };

                    Button editExpenseButton = new Button
                    {
                        Style = (Style)this.Resources["InvisibleButtonStyle"],
                        Width = 30,
                        Name = $"EditIncomeButton_{Guid.NewGuid():N}",
                        VerticalAlignment = VerticalAlignment.Top,
                        Content = new TextBlock
                        {
                            Text = "E",
                            FontSize = 20,
                            FontWeight = FontWeights.Bold,
                        },
                    };
                    editExpenseButton.Click += (sender, e) => this.EditExpense_Click(sender, e, currentCategoryIndex, currentIndexExpense);

                    Button deleteExpenseButton = new Button
                    {
                        Style = (Style)this.Resources["InvisibleButtonStyle"],
                        Height = 30,
                        Name = $"DeleteIncomeButton_{Guid.NewGuid():N}",
                        VerticalAlignment = VerticalAlignment.Top,
                        Content = new TextBlock
                        {
                            Text = "D",
                            FontSize = 20,
                            FontWeight = FontWeights.Bold,
                        },
                    };
                    deleteExpenseButton.Click += (sender, e) => this.DeleteExpense_Click(sender, e, currentCategoryIndex, currentIndexExpense);

                    newExpensePanel.Children.Add(newExpenseTitle);
                    newExpensePanel.Children.Add(spaceText);
                    newExpensePanel.Children.Add(newExpenseBudget);
                    newExpensePanel.Children.Add(editExpenseButton);
                    newExpensePanel.Children.Add(deleteExpenseButton);

                    categoryListView.Children.Add(newExpensePanel);
                }


                Button addCategoryExpenseButton = new Button
                {
                    Content = "Add Expense",
                    Style = (Style)this.Resources["InvisibleButtonStyle"],
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
                Button editCategoryButton = new Button
                {
                    Style = (Style)this.Resources["InvisibleButtonStyle"],
                    Width = 50,
                    Height = 50,
                    Name = $"EditIncomeButton_{Guid.NewGuid():N}",
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = new TextBlock
                    {
                        Text = "E",
                        FontSize = 40,
                        FontWeight = FontWeights.Bold,
                    },
                };
                editCategoryButton.Click += (sender, e) => this.EditCategory_Click(sender, e, currentCategoryIndex);

                Button deleteCategoryButton = new Button
                {
                    Style = (Style)this.Resources["InvisibleButtonStyle"],
                    Width = 50,
                    Height = 50,
                    Name = $"DeleteIncomeButton_{Guid.NewGuid().ToString("N")}",
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = new TextBlock
                    {
                        Text = "D",
                        FontSize = 40,
                        FontWeight = FontWeights.Bold,
                    },
                };
                deleteCategoryButton.Click += (sender, e) => this.DeleteCategory_Click(sender, e, currentCategoryIndex);
                StackPanel categoryHeaderPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                };
                categoryHeaderPanel.Children.Add(categoryHeader);
                categoryHeaderPanel.Children.Add(editCategoryButton);
                categoryHeaderPanel.Children.Add(deleteCategoryButton);
                newCategoryStackPanel.Children.Add(categoryHeaderPanel);
                newCategoryStackPanel.Children.Add(categoryListView);
                newCategoryBlock.Child = newCategoryStackPanel;

                // Find the last category and insert the new one below it
                var lastCategory = this.CategoriesListView.Children.OfType<Border>().LastOrDefault();
                if (lastCategory != null)
                {
                    int insertIndex = this.CategoriesListView.Children.IndexOf(lastCategory) + 1;
                    this.CategoriesListView.Children.Insert(insertIndex, newCategoryBlock);
                }
                else
                {
                    this.CategoriesListView.Children.Add(newCategoryBlock);
                }

                for (int j = 0; j < expenses[i].Expenses.Count; j++)
                {
                    // Створіть новий об'єкт "Expense"
                    Expense newExpense = new Expense(expenses[i].Expenses[j].ExpenseName, expenses[i].Expenses[j].Amount, categoryListView);

                    // Додайте новий "Expense" до категорії витрат (StackPanel)
                    if (categoryListView != null)
                    {
                        StackPanel newExpensePanel = new StackPanel();
                        newExpensePanel.Orientation = Orientation.Horizontal;

                        // Назва витрати
                        TextBlock newExpenseTitle = new TextBlock
                        {
                            Text = newExpense.Title,
                            FontSize = 20,
                            FontWeight = FontWeights.DemiBold,
                        };

                        // Пробіл
                        TextBlock spaceText = new TextBlock
                        {
                            Text = " ",
                            FontSize = 10,
                            FontWeight = FontWeights.DemiBold,
                            Foreground = Brushes.Gray,
                        };

                        // Бюджет
                        TextBlock newExpenseBudget = new TextBlock
                        {
                            Text = $"0/{newExpense.Amount} $",
                            FontSize = 20,
                            FontWeight = FontWeights.DemiBold,
                            Foreground = Brushes.Gray,
                            Height = 30,
                            VerticalAlignment = VerticalAlignment.Center,
                        };

                        // Додайте назву витрати, пробіл і бюджет в StackPanel
                        newExpensePanel.Children.Add(newExpenseTitle);
                        newExpensePanel.Children.Add(spaceText);
                        newExpensePanel.Children.Add(newExpenseBudget);

                        // Знайдіть кнопку "Add Expense" та вставте нову витрату перед нею
                        // for (int i = 0; i < expenseCategory.Children.Count; i++)
                        // {
                        //    if (expenseCategory.Children[i] is Button addExpenseButton)
                        //    {
                        //        expenseCategory.Children.Insert(i, newExpensePanel);
                        //        break;
                        //    }
                        // }
                    }
                }
            }
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            AddIncomePage addIncomePage = new AddIncomePage(this.IncomeListView);
            addIncomePage.ShowDialog();
        }

        private void AddSavings_Click(object sender, RoutedEventArgs e)
        {
            AddSavingsPage addSavingsPage = new AddSavingsPage(this.SavingsListView);
            addSavingsPage.ShowDialog();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryPage addCategoryPage = new AddCategoryPage(this); 
            addCategoryPage.ShowDialog();
        }

        public void AddCategory(Border newCategory)
        {
            // Додайте нову категорію на сторінку "Main" над кнопкою "Add Category"
            this.CategoriesListView.Children.Insert(this.CategoriesListView.Children.Count - 1, newCategory);
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            // Тут ви можете додати функціональність для кнопки "Add Transaction"
        }

        private bool buttonsVisible = false;

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            if (this.buttonsVisible)
            {
                // Приховати кнопки "Statistic", "Data export", "Tips and Tricks", "Settings" і "Sign out"
                // this.StatisticButton.Visibility = Visibility.Collapsed;
                this.DataExportButton.Visibility = Visibility.Collapsed;
                this.TipsAndTricksButton.Visibility = Visibility.Collapsed;
                this.SettingsButton.Visibility = Visibility.Collapsed;
                this.SignOutButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Показати кнопки "Statistic", "Data export", "Tips and Tricks", "Settings" і "Sign out"
                // this.StatisticButton.Visibility = Visibility.Visible;
                this.DataExportButton.Visibility = Visibility.Visible;
                this.TipsAndTricksButton.Visibility = Visibility.Visible;
                this.SettingsButton.Visibility = Visibility.Visible;
                this.SignOutButton.Visibility = Visibility.Visible;
            }

            this.buttonsVisible = !this.buttonsVisible;
        }

        private void SetIncomeList(List<Income> incomes)
        {
            for (int i = 0; i < incomes.Count; i++)
            {
                int currentIndex = i;
                StackPanel incomePanel = new StackPanel();
                incomePanel.Orientation = Orientation.Horizontal;

                TextBlock incomeTextBlock = new TextBlock()
                {
                    Text = incomes[i].IncomeName,
                    FontSize = 40,
                    FontWeight = FontWeights.DemiBold,
                };

                Button editIncomeButton = new Button
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
                editIncomeButton.Click += (sender, e) => this.EditIncome_Click(sender, e, currentIndex);

                Button deleteIncomeButton = new Button
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
                deleteIncomeButton.Click += (sender, e) => this.DeleteIncome_Click(sender, e, currentIndex);

                incomePanel.Children.Add(incomeTextBlock);
                incomePanel.Children.Add(editIncomeButton);
                incomePanel.Children.Add(deleteIncomeButton);

                this.IncomeListView.Children.Add(incomePanel);
            }
        }

        private void SetSavingsList(List<Saving> savings)
        {
            for (int i = 0; i < savings.Count; i++)
            {
                // Create a local variable inside the loop
                int currentIndex = i;

                StackPanel savingsPanel = new StackPanel();
                savingsPanel.Orientation = Orientation.Horizontal;

                TextBlock savingsTextBlock = new TextBlock()
                {
                    Text = savings[i].SavingName,
                    FontSize = 40,
                    FontWeight = FontWeights.DemiBold,
                };

                Button editSavingsButton = new Button
                {
                    Style = (Style)this.Resources["InvisibleButtonStyle"],
                    Width = 41,
                    Height = 41,
                    Name = $"EditSavingsButton_{Guid.NewGuid():N}",
                    Content = new TextBlock
                    {
                        Text = "E",
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                    },
                };
                editSavingsButton.Click += (sender, e) => this.EditSavings_Click(sender, e, currentIndex);

                Button deleteSavingsButton = new Button
                {
                    Style = (Style)this.Resources["InvisibleButtonStyle"],
                    Width = 41,
                    Height = 41,
                    Name = $"DeleteSavingsButton_{Guid.NewGuid().ToString("N")}",
                    Content = new TextBlock
                    {
                        Text = "D",
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                    },
                };
                deleteSavingsButton.Click += (sender, e) => this.DeleteSavings_Click(sender, e, currentIndex);

                savingsPanel.Children.Add(savingsTextBlock);
                savingsPanel.Children.Add(editSavingsButton);
                savingsPanel.Children.Add(deleteSavingsButton);

                this.SavingsListView.Children.Add(savingsPanel);
            }
        }

        private void EditIncome_Click(object sender, RoutedEventArgs e, int currentIndex)
        {
            EditncomePage addCategoryPage = new EditncomePage(this, this.incomes[currentIndex].Id);
            addCategoryPage.ShowDialog();
        }

        private void DeleteIncome_Click(object sender, RoutedEventArgs e, int currentIndex)
        {
            MessageBox.Show($"Delete clicked for {this.incomes[currentIndex].IncomeName}");
        }

        private void EditSavings_Click(object sender, RoutedEventArgs e, int currentIndex)
        {
            EditSavingsPage addCategoryPage = new EditSavingsPage(this, this.savings[currentIndex].Id);
            addCategoryPage.ShowDialog();
        }

        private void DeleteSavings_Click(object sender, RoutedEventArgs e, int currentIndex)
        {
            MessageBox.Show($"Delete clicked for {this.savings[currentIndex].SavingName}");
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e, int currentIndex)
        {
            EditCategoryPage addCategoryPage = new EditCategoryPage(this, this.expenses[currentIndex].ExpenseCategory.Id);
            addCategoryPage.ShowDialog();
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e, int currentIndex)
        {
            MessageBox.Show($"Delete clicked for {this.expenses[currentIndex].ExpenseCategory.CategoryName}");
        }

        private void EditExpense_Click(object sender, RoutedEventArgs e, int currentCategoryIndex, int currentIndexExpense)
        {
            MessageBox.Show($"Edit clicked for {this.expenses[currentCategoryIndex].Expenses[currentIndexExpense].ExpenseName}");
        }

        private void DeleteExpense_Click(object sender, RoutedEventArgs e, int currentCategoryIndex, int currentIndexExpense)
        {
            MessageBox.Show($"Delete clicked for {this.expenses[currentCategoryIndex].Expenses[currentIndexExpense].ExpenseName}");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            // UserManager.Instance.LogOutUser();
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