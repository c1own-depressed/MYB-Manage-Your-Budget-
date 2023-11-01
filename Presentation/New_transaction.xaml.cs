using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
namespace OtherPages
{
    /// <summary>
    /// Interaction logic for New_transaction.xaml
    /// </summary>
    public partial class New_transaction : Page
    {
        public New_transaction()
        {
            InitializeComponent();

       
            //string connectionString = "server=127.0.0.1;uid=root;pwd=1234;database=mybdb";

        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedCategory = (ComboBoxItem)CategoryComboBox.SelectedItem;
            string category = selectedCategory.Content.ToString();


            ComboBoxItem selectedParagraph = (ComboBoxItem)ParagraphComboBox.SelectedItem;
            string theme = selectedParagraph.Content.ToString();

            
            string cost = CostTextBox.Text;
            MessageBox.Show(category + theme + cost);
        }
    }
}
