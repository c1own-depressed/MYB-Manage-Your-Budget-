using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using MYB.DAL;
using DAL;
using BLL;

namespace MYB
{
    internal class Program
    {
       static string connectionString = "server=127.0.0.1;uid=root;pwd=1234;database=mybdb";
        static void Main()
        {
            using (var context = new AppDBContext())
            {
                var query = new Queries(context);

                UserQueries.AddUser("Romko53", "romkfdsaf28@gmail.com", "fsdafasdfsad");

                // Get a user by ID
                User user = query.GetUserById(1);
                if (user != null)
                {
                    Console.WriteLine($"User ID: {user.Id}, Username: {user.Username}, Email: {user.Email}");
                }
            }
        }
        //static void Main(string[] args)
        //{
        //    CreateDatabase();
        //    //FillDatabaseWithTestData();
        //    Console.WriteLine("Displaying User Table Data:");
        //    DisplayUserData();
        //    Console.WriteLine();
        //    Console.WriteLine("Displaying Expense Table Data:");
        //    DisplayExpenseData();
        //    Console.WriteLine();
        //    Console.WriteLine("Displaying ExpenseCategory Table Data:");
        //    DisplayExpenseCategoryData();
        //    Console.WriteLine();
        //    Console.WriteLine("Displaying Income Table Data:");
        //    DisplayIncomeData();
        //    Console.WriteLine();
        //    Console.WriteLine("Displaying Saving Table Data:");
        //    DisplaySavingData();
        //    Console.WriteLine();
        //    Console.WriteLine("Displaying Transaction Table Data:");
        //    DisplayTransactionData();
        //    Console.WriteLine();

        //}
        static void DisplayUserData()
        {
            

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM User";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Username: {reader["username"]}, Email: {reader["email"]}");
                    }
                }
            }
        }
        static void DisplayExpenseData()
        {
            

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Expense";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Expense_name: {reader["expense_name"]}, Amount: {reader["amount"]}, Expense_category_id: {reader["expense_category_id"]}");
                    }
                }
            }
        }
        static void DisplayIncomeData()
        {
           

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Income";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Income_name: {reader["income_name"]}, Amount: {reader["amount"]}, User_id: {reader["user_id"]}");
                    }
                }
            }
        }
        static void DisplaySavingData()
        {
           

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Saving";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Saving_name: {reader["saving_name"]}, Amount: {reader["amount"]}, User_id: {reader["user_id"]}");
                    }
                }
            }
        }
        static void DisplayExpenseCategoryData()
        {
         

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Expensecategory";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Category_name: {reader["category_name"]}, User_id: {reader["user_id"]}");
                    }
                }
            }
        }
        static void DisplayTransactionData()
        {
           

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Transaction";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Transaction_name: {reader["transaction_name"]},Amount: {reader["amount"]},Date: {reader["date"]}, Expense_id: {reader["expense_id"]}");
                    }
                }
            }
        }
        static void FillDatabaseWithTestData()
        {
   
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO User (username, password, email,light_theme, language, currency) VALUES (@username, @password, @email,@light_theme,@language,@currency)";

                for (int i = 0; i < 50; i++)
                {

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {

                    
                        insertCommand.Parameters.AddWithValue("@username", $"username {i}");
                        insertCommand.Parameters.AddWithValue("@password", $"password {i}");
                        insertCommand.Parameters.AddWithValue("@email", $"user{i}@gmail.com");
                        insertCommand.Parameters.AddWithValue("@light_theme", false);
                        insertCommand.Parameters.AddWithValue("@language", "ua");
                        insertCommand.Parameters.AddWithValue("@currency", "uan");
                        insertCommand.ExecuteNonQuery();
                    }
                        
                    }

                string insertQuery1 = "INSERT INTO Income (income_name, amount, user_id) VALUES (@income_name, @amount, @user_id)";

                for (int i = 0; i < 50; i++)
                {

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery1, connection))
                    {


                        insertCommand.Parameters.AddWithValue("@income_name", $"income {i}");
                        insertCommand.Parameters.AddWithValue("@amount", i*100+1);
                        insertCommand.Parameters.AddWithValue("@user_id", i+1 );
                        insertCommand.ExecuteNonQuery();
                    }

                }
                string insertQuery2 = "INSERT INTO Saving (saving_name, amount, user_id) VALUES (@saving_name, @amount, @user_id)";

                for (int i = 0; i < 50; i++)
                {

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery2, connection))
                    {


                        insertCommand.Parameters.AddWithValue("@saving_name", $"saving {i}");
                        insertCommand.Parameters.AddWithValue("@amount", i * 100 + 1);
                        insertCommand.Parameters.AddWithValue("@user_id", i + 1);
                        insertCommand.ExecuteNonQuery();
                    }

                }
                string insertQuery3 = "INSERT INTO ExpenseCategory (category_name, user_id) VALUES (@category_name, @user_id)";

                for (int i = 0; i < 50; i++)
                {

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery3, connection))
                    {


                        insertCommand.Parameters.AddWithValue("@category_name", $"category {i}");
                        insertCommand.Parameters.AddWithValue("@user_id", i + 1);
                        insertCommand.ExecuteNonQuery();
                    }

                }
                string insertQuery4 = "INSERT INTO Expense (expense_name,amount, expense_category_id) VALUES (@expense_name,@amount, @expense_category_id)";

                for (int i = 0; i < 50; i++)
                {

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery4, connection))
                    {


                        insertCommand.Parameters.AddWithValue("@expense_name", $"expense {i}");
                        insertCommand.Parameters.AddWithValue("@amount", i * 100+1);
                        insertCommand.Parameters.AddWithValue("@expense_category_id", i + 1);
                        insertCommand.ExecuteNonQuery();
                    }

                }
                string insertQuery5 = "INSERT INTO Transaction (transaction_name,amount,date, expense_id) VALUES (@transaction_name,@amount,@date, @expense_id)";

                for (int i = 0; i < 50; i++)
                {

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery5, connection))
                    {


                        insertCommand.Parameters.AddWithValue("@transaction_name", $"transaction {i}");
                        insertCommand.Parameters.AddWithValue("@amount", i * 100+1);
                        insertCommand.Parameters.AddWithValue("@date", DateTime.Now);
                        insertCommand.Parameters.AddWithValue("@expense_id", i + 1);
                        insertCommand.ExecuteNonQuery();
                    }

                }
            }
            }
        




        static public void CreateDatabase()
        {

        

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                
                string createTableUser =
                @"CREATE TABLE IF NOT EXISTS User(
	                id INT AUTO_INCREMENT NOT NULL,
	                username VARCHAR(30) NOT NULL,
                    CHECK (LENGTH(username) >= 5),
	                password VARCHAR(40) NOT NULL,
                    CHECK (LENGTH(password) >= 8),
	                email VARCHAR(100),
                    CHECK(email LIKE '%@%.%'),
	                light_theme bool DEFAULT(0),
                    language VARCHAR(2) DEFAULT('uk'),
                    CHECK (language IN ('ua', 'en')),
                    currency VARCHAR(3) DEFAULT('uan'),
                    CHECK (currency IN ('uan', 'usd')),
                    PRIMARY KEY (id)
                );";

                string createTableIncome =
                @"CREATE TABLE IF NOT EXISTS Income(
	                id INT AUTO_INCREMENT NOT NULL,
	                income_name VARCHAR(100) NOT NULL,
                    CHECK(LENGTH(income_name) >= 5),
	                amount INT,
                    CHECK(amount > 0 AND amount < 100000000),
                    user_id INT,
                    PRIMARY KEY (id),
                    FOREIGN KEY (user_id) REFERENCES User(id)
                );";

                string createTableSaving =
                @"CREATE TABLE IF NOT EXISTS Saving(
	                id INT AUTO_INCREMENT NOT NULL,
	                saving_name VARCHAR(100) NOT NULL,
                    CHECK(LENGTH(saving_name) >= 5),
	                amount int,
                    CHECK(amount > 0 AND amount <= 10000000000000000),
                    user_id INT,
                    PRIMARY KEY (id),
                    FOREIGN KEY (user_id) REFERENCES User(id)
                );";

                string createTableExpenseCategory =
                @"CREATE TABLE IF NOT EXISTS ExpenseCategory(
	                id INT AUTO_INCREMENT NOT NULL,
                    category_name VARCHAR(100),
                    CHECK(LENGTH(category_name) >= 5),
                    user_id INT,
                    PRIMARY KEY (id),
	                FOREIGN KEY (user_id) REFERENCES User(id)
                );";

                string createTableExpense =
                @"CREATE TABLE IF NOT EXISTS Expense(
	                id INT AUTO_INCREMENT NOT NULL,
                    expense_name VARCHAR(100),
                    CHECK(LENGTH(expense_name) >= 5),
                    amount INT,
                    CHECK(amount > 0 AND amount <= 100000000),
                    expense_category_id INT,
                    PRIMARY KEY (id),
	                FOREIGN KEY (expense_category_id) REFERENCES ExpenseCategory(id)
                );";

                string createTableTransaction =
                @"CREATE TABLE IF NOT EXISTS Transaction(
	                id INT AUTO_INCREMENT NOT NULL,
	                transaction_name VARCHAR(100),
                    CHECK (LENGTH(transaction_name) >= 5),
                    amount int,
                    CHECK(amount > 0 AND amount <= 100000000),
	                date TIMESTAMP,
                    expense_id INT,
                    PRIMARY KEY (id),
                    FOREIGN KEY (expense_id) REFERENCES Expense(id)
                );";

             
                using (MySqlCommand command = new MySqlCommand(createTableUser, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command = new MySqlCommand(createTableIncome, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command = new MySqlCommand(createTableSaving, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command = new MySqlCommand(createTableExpenseCategory, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command = new MySqlCommand(createTableExpense, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command = new MySqlCommand(createTableTransaction, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}