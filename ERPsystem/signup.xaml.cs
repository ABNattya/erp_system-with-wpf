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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace ERPsystem
{
    /// <summary>
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : Window
    {
        public signup()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-HF9IEV1\MSSQLSERVER2;Initial Catalog=erp_system;Integrated Security=True");

        private void GoToLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MA = new MainWindow();
            MA.Show();
            this.Close();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {

            var email = emailbox.Text;
            Regex regex = new Regex(@"^[0-9a-zA-z]{4,15}(@)(01electronics)(.net)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);


            bool isValidEmail = regex.IsMatch(email);


            if (isValidEmail)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO employees_business_emails (EmailAddress) VALUES (@EmailAddress)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EmailAddress", email);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Registered Go to Login now", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow MA = new MainWindow();
                MA.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email invalid must be ended with @01electronics.net ", "Faild", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
