using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MYB_NEW;

namespace reg
{
    /// <summary>
    /// Interaction logic for Statistic1.xaml
    /// </summary>
    public partial class Statistic1 : Page
    {
        public Statistic1()
        {
            InitializeComponent();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }
        public class DataRecord
        {
            public string Date { get; set; }
            public int Income { get; set; }
            public int SummaryExpenses { get; set; }
            public int Saved { get; set; }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //List<DataRecord> records = GetRecordsFromDatabase(); // треба дописати метод який б зчитував то все з таблиці

            //for (int i = 0; i < records.Count; i++)
            //{
            //    DataRecord record = records[i];

            //    TextBlock dateTextBlock = new TextBlock();
            //    dateTextBlock.Text = record.Date.ToString();
            //    dateTextBlock.FontFamily = new FontFamily("Inter");
            //    dateTextBlock.TextAlignment = TextAlignment.Center;
            //    dateTextBlock.FontSize = 20;
            //    dateTextBlock.Height = 30;
            //    dateTextBlock.VerticalAlignment = VerticalAlignment.Top;
            //    dateTextBlock.Margin = new Thickness(100, 100 + i * 40, 906, 0);

            //    TextBlock incomeTextBlock = new TextBlock();
            //    incomeTextBlock.Text = record.Income.ToString();
            //    incomeTextBlock.FontFamily = new FontFamily("Inter");
            //    incomeTextBlock.TextAlignment = TextAlignment.Center;
            //    incomeTextBlock.FontSize = 20;
            //    incomeTextBlock.Height = 30;
            //    incomeTextBlock.VerticalAlignment = VerticalAlignment.Top;
            //    incomeTextBlock.Margin = new Thickness(320, 100 + i * 40, 685, 0);

            //    TextBlock summaryexpenses = new TextBlock();
            //    summaryexpenses.Text = record.SummaryExpenses.ToString();
            //    summaryexpenses.FontFamily = new FontFamily("Inter");
            //    summaryexpenses.TextAlignment = TextAlignment.Center;
            //    summaryexpenses.FontSize = 20;
            //    summaryexpenses.Height = 30;
            //    summaryexpenses.VerticalAlignment = VerticalAlignment.Top;
            //    summaryexpenses.Margin = new Thickness(320, 100 + i * 40, 685, 0);

            //    TextBlock saved = new TextBlock();
            //    saved.Text = record.Saved.ToString();
            //    saved.FontFamily = new FontFamily("Inter");
            //    saved.TextAlignment = TextAlignment.Center;
            //    saved.FontSize = 20;
            //    saved.Height = 30;
            //    saved.VerticalAlignment = VerticalAlignment.Top;
            //    saved.Margin = new Thickness(320, 100 + i * 40, 685, 0);

            //    Grid myGrid = FindName("Grid") as Grid;
            //    myGrid.Children.Add(dateTextBlock);
            //    myGrid.Children.Add(incomeTextBlock);
            //    myGrid.Children.Add(summaryexpenses);
            //    myGrid.Children.Add(saved);
            //}
        }
    }
}

//з бота
//private string connectionString = "YourConnectionStringHere"; // Змініть на свої дані підключення

//public List<DataRecord> GetRecordsFromDatabase()
//{
//    List<DataRecord> records = new List<DataRecord>();
//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        string query = "SELECT Date, Income, SummaryExpenses, Saved FROM YourTableName"; // Змініть на свої назви таблиці та колонок
//        SqlCommand command = new SqlCommand(query, connection);

//        connection.Open();
//        SqlDataReader reader = command.ExecuteReader();

//        while (reader.Read())
//        {
//            DataRecord record = new DataRecord();
//            record.Date = reader["Date"].ToString();
//            record.Income = Convert.ToInt32(reader["Income"]);
//            record.SummaryExpenses = Convert.ToInt32(reader["SummaryExpenses"]);
//            record.Saved = Convert.ToInt32(reader["Saved"]);

//            records.Add(record);
//        }

//        reader.Close();
//    }

//    return records;
//}