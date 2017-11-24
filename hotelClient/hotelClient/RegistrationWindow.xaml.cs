using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApplication2;

namespace hotelClient
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetCities", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();
                while(data.Read())
                {
                    this.City.Items.Add(data[0].ToString());
                }
                cn.Close();
            }
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validator.ValidTextBoxes(this.City.Text, this.HotelName.Text, this.Pass.Password, this.Stars.Text))
                {
                    using (SqlConnection cn = Connector.GetConnection())
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("Registration", cn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter city = new SqlParameter();
                        city.ParameterName = "@city";
                        city.Value = this.City.Text;

                        SqlParameter hotel = new SqlParameter();
                        hotel.ParameterName = "@name";
                        hotel.Value = this.HotelName.Text;

                        SqlParameter pass = new SqlParameter();
                        pass.ParameterName = "@password";
                        pass.Value = this.Pass.Password;

                        SqlParameter stars = new SqlParameter();
                        stars.ParameterName = "@stars";
                        stars.SqlDbType = System.Data.SqlDbType.Int;
                        int star = Convert.ToInt32(this.Stars.Text);
                        stars.Value = star;
                        
                        cmd.Parameters.Add(hotel);
                        cmd.Parameters.Add(stars);
                        cmd.Parameters.Add(city);
                        cmd.Parameters.Add(pass);

                        SqlParameter rc = new SqlParameter();
                        rc.ParameterName = "@rc";
                        rc.SqlDbType = System.Data.SqlDbType.Bit;
                        rc.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(rc);

                        cmd.ExecuteNonQuery();

                        cn.Close();

                        if ((bool)cmd.Parameters["@rc"].Value)
                        {
                            MessageBox.Show("Hotel registration SUCCESSFULY");
                            MainWindow mainWnd = new MainWindow();
                            mainWnd.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("Error in registration Hotel");
                    }
                }
                else
                {
                    MessageBox.Show("Enter data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
