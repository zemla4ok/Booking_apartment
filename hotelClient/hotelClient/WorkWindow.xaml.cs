﻿using System;
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
        private string FileNameExport { get; set; }

        public WorkWindow(string pCity, string pHotel)
        {
            InitializeComponent();
            this.CityName = pCity;
            this.HotelName = pHotel;
            this.GetApartments();
            this.GetBookedApartments();
            this.SetEvicDate.DisplayDateStart = DateTime.Today.AddDays(1);
        }

        private void AddApartment(object sender, RoutedEventArgs e)
        {
            try
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

                    SqlParameter is_close = new SqlParameter();
                    is_close.ParameterName = "@is_close";
                    is_close.SqlDbType = System.Data.SqlDbType.Bit;
                    is_close.Value = this.Yes.IsChecked == true ? true : false;

                    cmd.Parameters.Add(currCost);
                    cmd.Parameters.Add(places);
                    cmd.Parameters.Add(freePlaces);
                    cmd.Parameters.Add(hotel);
                    cmd.Parameters.Add(city);
                    cmd.Parameters.Add(apartNum);
                    cmd.Parameters.Add(is_close);

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
                using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-M13O155;Initial Catalog=BookingApartment;Integrated Security=True"))
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
                    apartNum.ParameterName = "@apart_num";
                    apartNum.Value = this.ApartNumUpd.Text;

                    SqlParameter is_close = new SqlParameter();
                    is_close.ParameterName = "@is_close";
                    is_close.Value = this.YesUp.IsChecked == true ? true : false;

                    cmd.Parameters.Add(currCost);
                    cmd.Parameters.Add(places);
                    cmd.Parameters.Add(freePlaces);
                    cmd.Parameters.Add(hotel);
                    cmd.Parameters.Add(city);
                    cmd.Parameters.Add(apartNum);
                    cmd.Parameters.Add(is_close);

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

        private void GetApartments()
        {
            TextBlock tb = new TextBlock();
            tb.Style = this.FindResource("1") as Style;
            tb.Text = "CURRENT COST     PLACES      FREE PLACES     APARTMENT NUM             IS CLOSE";
            this.Apartments.Children.Add(tb);


            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetApartmentsForThisHotel", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter hotel = new SqlParameter();
                hotel.ParameterName = "@hotel";
                hotel.Value = this.HotelName;

                SqlParameter city = new SqlParameter();
                city.ParameterName = "@city";
                city.Value = this.CityName;

                cmd.Parameters.Add(hotel);
                cmd.Parameters.Add(city);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    string str;
                    if (Convert.ToBoolean(data[4].ToString()))
                        str = "Yes";
                    else
                        str = "No";
                    tb = new TextBlock();
                    tb.Style = this.FindResource("1") as Style;
                    tb.Text = "\t" + data[0].ToString() +
                              "    \t        " + data[1].ToString() +
                              "  \t\t  " + data[2].ToString() +
                              "     \t     \t     " + data[3].ToString() +
                              "\t\t        " + str;
                    this.Apartments.Children.Add(tb);
                }
                cn.Close();
            }
        }

        private void SortApartments(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Apartments.Children.Clear();

                TextBlock tb = new TextBlock();
                tb.Style = this.FindResource("1") as Style;
                tb.Text = "CURRENT COST     PLACES      FREE PLACES     APARTMENT NUM       IS CLOSE";
                this.Apartments.Children.Add(tb);


                using (SqlConnection cn = Connector.GetConnection())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SortApartments", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter hotel = new SqlParameter();
                    hotel.ParameterName = "@hotel";
                    hotel.Value = this.HotelName;

                    SqlParameter city = new SqlParameter();
                    city.ParameterName = "@city";
                    city.Value = this.CityName;

                    cmd.Parameters.Add(hotel);
                    cmd.Parameters.Add(city);

                    SqlParameter curr_cost = new SqlParameter();
                    curr_cost.ParameterName = "@curr_cost";
                    curr_cost.Value = this.CURR_COST.IsChecked;

                    SqlParameter places = new SqlParameter();
                    places.ParameterName = "@places";
                    places.Value = this.PLACES.IsChecked;

                    SqlParameter free_places = new SqlParameter();
                    free_places.ParameterName = "@free_places";
                    free_places.Value = this.FREE_PLACES.IsChecked;

                    SqlParameter apart_num = new SqlParameter();
                    apart_num.ParameterName = "@apart_num";
                    apart_num.Value = this.APART_NUM.IsChecked;

                    SqlParameter is_close = new SqlParameter();
                    is_close.ParameterName = "@is_close";
                    is_close.Value = this.CL_DATE.IsChecked;

                    cmd.Parameters.Add(curr_cost);
                    cmd.Parameters.Add(places);
                    cmd.Parameters.Add(free_places);
                    cmd.Parameters.Add(apart_num);
                    cmd.Parameters.Add(is_close);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader data = cmd.ExecuteReader();

                    while (data.Read())
                    {
                        string str;
                        if (Convert.ToBoolean(data[4].ToString()))
                            str = "Yes";
                        else
                            str = "No";
                        tb = new TextBlock();
                        tb.Style = this.FindResource("1") as Style;
                        tb.Text = "\t" + data[0].ToString() +
                                  "    \t        " + data[1].ToString() +
                                  "  \t\t  " + data[2].ToString() +
                                  "     \t     \t     " + data[3].ToString() +
                                  "\t\t  " + str;
                        this.Apartments.Children.Add(tb);
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void GetBookedApartments()
        {
            this.BookedApartments.Children.Clear();
            TextBlock tb = new TextBlock();
            tb.Style = this.FindResource("1") as Style;
            tb.Text = "ID" + "\t" + "APART NUM" + "\t" + "COST" + "\t" + "PLACES" + "\t   " + 
                "ARR DATE" + "\t" + "EVIC DATE" + "\t" + "RESERV DATE" + "\t" + "EARLY" + "\t" + "EVICTED";           
            this.BookedApartments.Children.Add(tb);

            try
            {
                using (SqlConnection cn = Connector.GetConnection())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("GetBookingListForCurrentHotel", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter hotel = new SqlParameter();
                    hotel.ParameterName = "@hotel";
                    hotel.Value = this.HotelName;

                    SqlParameter city = new SqlParameter();
                    city.ParameterName = "@city";
                    city.Value = this.CityName;

                    SqlParameter is_evic = new SqlParameter();
                    is_evic.ParameterName = "@is_evic";
                    is_evic.Value = this.IsEvicted.IsChecked == true ? true : false;

                    cmd.Parameters.Add(hotel);
                    cmd.Parameters.Add(city);
                    cmd.Parameters.Add(is_evic);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader data = cmd.ExecuteReader();

                    while (data.Read())
                    {
                        string str;
                        if (Convert.ToBoolean(data[6].ToString()))
                            str = "Yes";
                        else
                            str = "No";
                        string str1;
                        if (Convert.ToBoolean(data[8].ToString()))
                            str1 = "Yes";
                        else
                            str1 = "No";
                        tb = new TextBlock();
                        tb.Style = this.FindResource("1") as Style;
                        tb.Text = data[7].ToString() +
                                  "\t        " + data[0].ToString() +
                                  "\t\t" + data[1].ToString() +
                                  "\t     " + data[2].ToString() +
                                  "\t" + data[3].ToString().Substring(0, 10) +
                                  "\t" + data[4].ToString().Substring(0, 10) +
                                  "\t  " + data[5].ToString().Substring(0, 10) +
                                  "\t   " + str +
                                  "\t    " + str1;
                        this.BookedApartments.Children.Add(tb);
                    }
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void RefreshBookedList(object sender, RoutedEventArgs e)
        {
            this.GetBookedApartments();
        }

        private void GetSum(object sender, RoutedEventArgs e)
        {

            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetSumForEviction", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter ord_num = new SqlParameter();
                ord_num.ParameterName = "@book_id";
                ord_num.Value = this.OrdNum.Text;
                cmd.Parameters.Add(ord_num);

                SqlParameter rc = new SqlParameter();
                rc.ParameterName = "@rc";
                rc.SqlDbType = System.Data.SqlDbType.Int;
                rc.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(rc);

                cmd.ExecuteNonQuery();
                cn.Close();

                this.Sum.Text = cmd.Parameters["@rc"].Value.ToString();
                if (cmd.Parameters["@rc"].Value.ToString().Length != 0)
                    this.Evict.IsEnabled = true;
            }
        }

        private void EvictClient(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection cn = Connector.GetConnection())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("EvictClient", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter ord_num = new SqlParameter();
                    ord_num.ParameterName = "@book_id";
                    ord_num.Value = this.OrdNum.Text;
                    cmd.Parameters.Add(ord_num);

                    SqlParameter rc = new SqlParameter();
                    rc.ParameterName = "@rc";
                    rc.SqlDbType = System.Data.SqlDbType.Bit;
                    rc.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(rc);

                    cmd.ExecuteNonQuery();
                    cn.Close();

                    if ((bool)cmd.Parameters["@rc"].Value)
                        System.Windows.MessageBox.Show("Eviction Successfull");
                    else
                        System.Windows.MessageBox.Show("Error");
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show("error");
            }
        }

        private void SettleClient(object sender, RoutedEventArgs e)
        {
            if(Validator.ValidTextBoxes(this.SetEvicDate.Text, this.SetName.Text, this.SetSurname.Text, this.SetPasspNum.Text,
                this.SetApartNum.Text, this.SetPlaces.Text))
            {
                try
                {
                    using (SqlConnection cn = Connector.GetConnection())
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("SettleClient", cn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@city_name", this.CityName);
                        cmd.Parameters.AddWithValue("@hotel_name", this.HotelName);
                        cmd.Parameters.AddWithValue("@apart_num", this.SetApartNum.Text);
                        cmd.Parameters.AddWithValue("@user_name", this.SetName.Text);
                        cmd.Parameters.AddWithValue("@user_surname", this.SetSurname.Text);
                        cmd.Parameters.AddWithValue("@passport_num", this.SetPasspNum.Text);
                        cmd.Parameters.AddWithValue("@reserv_places", this.SetPlaces.Text);
                        cmd.Parameters.AddWithValue("@is_doseage", this.SetIsDos.IsChecked == true ? false : true);
                        cmd.Parameters.AddWithValue("@evic_date", this.SetEvicDate.Text);

                        SqlParameter rc = new SqlParameter();
                        rc.ParameterName = "@rc";
                        rc.SqlDbType = System.Data.SqlDbType.Bit;
                        rc.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(rc);

                        cmd.ExecuteNonQuery();
                        cn.Close();

                        if ((bool)cmd.Parameters["@rc"].Value)
                            System.Windows.MessageBox.Show("Settling Successfull");
                        else
                            System.Windows.MessageBox.Show("Error");
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Enter data");
            }
        }

        private void ChooseApartToExport(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == true)
            {
                this.FileNameExport = dlg.FileName;
                this.Path2.Text = dlg.FileName;
                this.ExportToXML.IsEnabled = true;
            }
        }

        private void OnExportToXML(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-M13O155;Initial Catalog=BookingApartment;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ExportToXML", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@path", this.FileNameExport);

                    SqlParameter rc = new SqlParameter();
                    rc.ParameterName = "@rc";
                    rc.SqlDbType = System.Data.SqlDbType.Bit;
                    rc.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(rc);

                    cmd.ExecuteNonQuery();
                    cn.Close();

                    if ((bool)cmd.Parameters["@rc"].Value)
                        System.Windows.MessageBox.Show("Export is Successfull");
                    else
                        System.Windows.MessageBox.Show("Error");
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}