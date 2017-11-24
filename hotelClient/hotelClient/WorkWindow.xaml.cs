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
using WpfApplication2;

namespace hotelClient
{
    /// <summary>
    /// Interaction logic for WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        private string CityName { get; set; }
        private string HotelName { get; set; }

        public WorkWindow(string pCity, string pHotel)
        {
            InitializeComponent();
            this.CityName = pCity;
            this.HotelName = pHotel;
        }

        private void AddApartment(object sender, RoutedEventArgs e)
        {
                if (Validator.ValidTextBoxes(this.CloseDate.Text))
                {
                    using (SqlConnection cn = Connector.GetConnection())
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("AddApartment", cn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        SqlParameter currCost = new SqlParameter();
                        currCost.ParameterName = "@curr_cost";
                        currCost.Value = this.CurrCost.Text;

                        SqlParameter places = new SqlParameter();
                        places.ParameterName = "@places";
                        places.Value = this.Places.Text;

                        SqlParameter freePlaces = new SqlParameter();
                        freePlaces.ParameterName = "@free_places";
                        freePlaces.Value = this.FreePlaces.Text;

                        SqlParameter hotel = new SqlParameter();
                        hotel.ParameterName = "@hotel";
                        hotel.Value = this.HotelName;

                        SqlParameter city = new SqlParameter();
                        city.ParameterName = "@city";
                        city.Value = this.CityName;

                        SqlParameter apartNum = new SqlParameter();
                        apartNum.ParameterName = "@apartment_num";
                        apartNum.Value = this.ApartNum.Text;

                        SqlParameter closeDate = new SqlParameter();
                        closeDate.ParameterName = "@close_date";
                        closeDate.Value = this.CloseDate.Text;

                        cmd.Parameters.Add(currCost);
                        cmd.Parameters.Add(places);
                        cmd.Parameters.Add(freePlaces);
                        cmd.Parameters.Add(hotel);
                        cmd.Parameters.Add(city);
                        cmd.Parameters.Add(apartNum);
                        cmd.Parameters.Add(closeDate);

                        SqlParameter rc = new SqlParameter();
                        rc.ParameterName = "@rc";
                        rc.SqlDbType = System.Data.SqlDbType.Bit;
                        rc.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(rc);

                        cmd.ExecuteNonQuery();
                        cn.Close();

                        if ((bool)cmd.Parameters["@rc"].Value)
                            MessageBox.Show("Adding apartments complete");
                        else
                            MessageBox.Show("Adding error");
                    }
                }
                else
                {
                    MessageBox.Show("Input Close date");
                }
        }
    }
}
