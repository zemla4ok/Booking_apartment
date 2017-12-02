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
            //load stars

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
            this.Stars.Items.Add("1");
            this.Stars.Items.Add("2");
            this.Stars.Items.Add("3");
            this.Stars.Items.Add("4");
            this.Stars.Items.Add("5");
            this.Stars.IsEnabled = true;
        }

        private void OnChangeStars(object sender, SelectionChangedEventArgs e)
        {
            this.Apartments.Items.Clear();
            this.Places.Items.Clear();
            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetApartmentsByHotelStars", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter city = new SqlParameter();
                city.ParameterName = "@city";
                city.Value = this.City.Text;
                cmd.Parameters.Add(city);

                SqlParameter stars = new SqlParameter();
                stars.ParameterName = "@stars";
                stars.Value = this.Stars.SelectedItem.ToString();
                cmd.Parameters.Add(stars);

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

            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetUniqPlacesByCityAndStars", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter city = new SqlParameter();
                city.ParameterName = "@city";
                city.Value = this.City.Text;
                cmd.Parameters.Add(city);

                SqlParameter stars = new SqlParameter();
                stars.ParameterName = "@stars";
                stars.Value = this.Stars.SelectedItem.ToString();
                cmd.Parameters.Add(stars);

                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    this.Places.Items.Add(data[0].ToString());
                }
                cn.Close();
            }
            this.Places.IsEnabled = true;
        }





    }
}