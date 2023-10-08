using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace MYB
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CreateDatabase();

        }


        static public void CreateDatabase()
        {

            string connectionString = "server=127.0.0.1;uid=root;pwd=uTnw0PIh65_!;database=mybdb";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // commands to create tables
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

                // creating tables using connection configuration and a command
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