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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
namespace ERPsystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-HF9IEV1\MSSQLSERVER2;Initial Catalog=erp_system;Integrated Security=True");


        private void LoginBt_Click(object sender, RoutedEventArgs e)
        {
            var email = emailtxt.Text;
            Regex regex = new Regex(@"^[0-9a-zA-z]{4,15}(@)(01electronics)(.net)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
           

            bool isValidEmail = regex.IsMatch(email);
   

            if (isValidEmail)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(1) from employees_business_emails where EmailAddress = '"+email+"' ", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (count==1)
                {
                    displayWindow ds = new displayWindow();
                    ds.Show();
                    this.Close();
                   

                }
                else
                {
                    MessageBox.Show("Email is incorrect","Faild",MessageBoxButton.OK,MessageBoxImage.Error);
                }


            }
            else
            {
                MessageBox.Show("Email invalid ", "Faild", MessageBoxButton.OK, MessageBoxImage.Error);
            }

         
        }

        private void CreateNewbtn_Click(object sender, RoutedEventArgs e)
        {
            signup si = new signup();
            si.Show();
            this.Close();
        }
    }
}
