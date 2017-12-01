using hotelClient;
using Newtonsoft.Json;
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

namespace CustomerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //load all apartments
            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetAllApartments", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    Apartment ap = new Apartment(data[0].ToString(), data[1].ToString(),
                        Convert.ToInt32(data[2].ToString()), Convert.ToInt32(data[3].ToString()),
                        Convert.ToInt32(data[4].ToString()), Convert.ToInt32(data[5].ToString()),
                        Convert.ToInt32(data[6].ToString()), data[7].ToString().Substring(0, 10));
                    this.Apartments.Items.Add(ap);
                }
                cn.Close();
            }
            //load cityes
            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetCities", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    this.City.Items.Add(data[0].ToString());
                }
                cn.Close();
            }
        }

        private void Apart_DoubleClick(object sender, RoutedEventArgs e)
        {
            string ap = JsonConvert.SerializeObject((Apartment)this.Apartments.CurrentItem);
            OrderApartmenwWindow oaw = new OrderApartmenwWindow(ap);
            oaw.Show();
        }

        private void GetCityHotels(object sender, SelectionChangedEventArgs e)
        {
            this.Apartments.Items.Clear();
            this.Hotel.Items.Clear();
            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetHotelsForChosedCity", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter city = new SqlParameter();
                city.ParameterName = "@city";
                city.Value = this.City.SelectedItem.ToString();
                cmd.Parameters.Add(city);

                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    Apartment ap = new Apartment(data[0].ToString(), data[1].ToString(),
                        Convert.ToInt32(data[2].ToString()), Convert.ToInt32(data[3].ToString()),
                        Convert.ToInt32(data[4].ToString()), Convert.ToInt32(data[5].ToString()),
                        Convert.ToInt32(data[6].ToString()), data[7].ToString().Substring(0, 10));
                    this.Apartments.Items.Add(ap);
                }                
                cn.Close();
            }
            try
            {
                using (SqlConnection cn = Connector.GetConnection())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("GetUniqHotelsForChosedCity", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter city = new SqlParameter();
                    city.ParameterName = "@city";
                    city.Value = this.City.SelectedItem.ToString();
                    cmd.Parameters.Add(city);

                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        this.Hotel.Items.Add(data[0].ToString());
                    }
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Hotel.IsEnabled = true;
        }
    }
}