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
using System.Windows.Forms;
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
        private string FileName { get; set; }

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
                        System.Windows.MessageBox.Show("Adding apartments complete");
                    else
                        System.Windows.MessageBox.Show("Adding error");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Input Close date");
            }
        }

        private void ChoosePathOfApartXML(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == true)
            {
                this.FileName = dlg.FileName;
                this.Path.Text = dlg.FileName;
                this.AddApartXML.IsEnabled = true;
            }
        }

        private void AddApartmentsFromXLMFile(object sender, RoutedEventArgs e)
        {
            try
            {


                using (SqlConnection cn = Connector.GetConnection())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("addApartmentsFromXML", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter path = new SqlParameter();
                    path.ParameterName = "@path";
                    path.SqlDbType = System.Data.SqlDbType.NVarChar;
                    path.Size = 256;
                    path.Value = this.FileName;

                    SqlParameter hotel = new SqlParameter();
                    hotel.ParameterName = "@hotel";
                    hotel.Value = this.HotelName;

                    SqlParameter city = new SqlParameter();
                    city.ParameterName = "@city";
                    city.Value = this.CityName;

                    cmd.Parameters.Add(path);
                    cmd.Parameters.Add(city);
                    cmd.Parameters.Add(hotel);

                    SqlParameter rc = new SqlParameter();
                    rc.ParameterName = "@rc";
                    rc.SqlDbType = System.Data.SqlDbType.Bit;
                    rc.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(rc);

                    cmd.ExecuteNonQuery();
                    cn.Close();

                    if ((bool)cmd.Parameters["@rc"].Value)
                        System.Windows.MessageBox.Show("Adding apartments complete");
                    else
                        System.Windows.MessageBox.Show("Adding error");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void UpdateApartment(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection cn = Connector.GetConnection())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("UpdateApartment", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    SqlParameter currCost = new SqlParameter();
                    currCost.ParameterName = "@curr_cost";
                    currCost.Value = this.CurrCostUpd.Text;

                    SqlParameter places = new SqlParameter();
                    places.ParameterName = "@places";
                    places.Value = this.PlacesUpd.Text;

                    SqlParameter freePlaces = new SqlParameter();
                    freePlaces.ParameterName = "@free_places";
                    freePlaces.Value = this.FreePlacesUpd.Text;

                    SqlParameter hotel = new SqlParameter();
                    hotel.ParameterName = "@hotel";
                    hotel.Value = this.HotelName;

                    SqlParameter city = new SqlParameter();
                    city.ParameterName = "@city";
                    city.Value = this.CityName;

                    SqlParameter apartNum = new SqlParameter();
                    apartNum.ParameterName = "@apartt_num";
                    apartNum.Value = this.ApartNumUpd.Text;

                    SqlParameter closeDate = new SqlParameter();
                    closeDate.ParameterName = "@close_date";
                    closeDate.Value = this.CloseDateUpd.Text;

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
                        System.Windows.MessageBox.Show("Update apartments complete");
                    else
                        System.Windows.MessageBox.Show("Updating error");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }




    }
}
