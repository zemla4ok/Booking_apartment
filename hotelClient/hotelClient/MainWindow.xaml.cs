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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication2;

namespace hotelClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validator.ValidTextBoxes(this.City.Text, this.Hotel.Text))
                {
                    using (SqlConnection cn = Connector.GetConnection())
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("Authorisation", cn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter city = new SqlParameter();
                        city.ParameterName = "@city";
                        city.Value = this.City.Text;

                        SqlParameter hotel = new SqlParameter();
                        hotel.ParameterName = "@hotel";
                        hotel.Value = this.Hotel.Text;

                        SqlParameter pass = new SqlParameter();
                        pass.ParameterName = "@password";
                        pass.Value = this.Pass.Password;

                        cmd.Parameters.Add(city);
                        cmd.Parameters.Add(hotel);
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
                            WorkWindow workWnd = new WorkWindow(this.City.Text, this.Hotel.Text);
                            workWnd.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("Authorisation error");
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

        private void Registration(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWind = new RegistrationWindow();
            regWind.Show();
            this.Close();
        }
    }
}
