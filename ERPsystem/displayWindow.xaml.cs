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
using System.Data.SqlClient;
using System.Data;

namespace ERPsystem
{
    /// <summary>
    /// Interaction logic for displayWindow.xaml
    /// </summary>
    public partial class displayWindow : Window
    {
        public displayWindow()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-HF9IEV1\MSSQLSERVER2;Initial Catalog=erp_system;Integrated Security=True");

        public void viewingraid()
        {
            SqlCommand sd = new SqlCommand("select * From company_name ", con);
            SqlCommand sd2 = new SqlCommand("select address From company_address ", con);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            con.Open();
            SqlDataReader sdr = sd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            con.Open();
            SqlDataReader sdr2 = sd2.ExecuteReader();
           
            dt2.Load(sdr2);
            con.Close();


            dataview.ItemsSource = dt.DefaultView;
            adressview.ItemsSource=dt2.DefaultView;
        }

        public void clearallTextbox()
        {
            compIDtext.Clear();
            comptext.Clear();
            compadress.Clear();
    

        }

        public bool validOrnot()
        {

     

            if (compadress.Text == String.Empty && comptext.Text == String.Empty)
            {
                MessageBox.Show(" at least company name or contact is requird if you want search for compnies or branches", "Faild", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
     
            return true;

        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            if(validOrnot())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO company_name (company_name) VALUES (@company_name)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@company_name", comptext.Text);

                SqlCommand sec = new SqlCommand("INSERT INTO company_address (address) VALUES (@add) ", con);
                sec.CommandType = CommandType.Text;
                sec.Parameters.AddWithValue("@add", compadress.Text);

                con.Open();
                sec.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                con.Close();

                viewingraid();
                MessageBox.Show("Successfully Added", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                clearallTextbox();
                
            }

        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM company_name WHERE company_serial=" + Convert.ToInt64(compIDtext.Text.ToString()) + "", con);
            SqlCommand cmd2 = new SqlCommand("DELETE FROM company_address WHERE address_serial=" + Convert.ToInt64(compIDtext.Text.ToString()) + "", con);


            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully deleted", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                clearallTextbox();
                viewingraid();
                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not deleted"+ex.Message);

            }
            finally
            {
                con.Close();

            }


        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE company_name SET company_name= '"+comptext.Text+"' WHERE company_serial= '" + Convert.ToInt64(compIDtext.Text.ToString()) + "' ", con);
            SqlCommand cmd2 = new SqlCommand("UPDATE company_address SET address= '" + compadress.Text + "' WHERE address_serial= '" + Convert.ToInt64(compIDtext.Text.ToString()) + "' ", con);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                clearallTextbox();
                viewingraid();
                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not updated yet " + ex.Message);

            }
            finally
            {
                con.Close();

            }
        }


        private void ViewBTN_Click_1(object sender, RoutedEventArgs e)
        {
            viewingraid();
        }
    }
}
