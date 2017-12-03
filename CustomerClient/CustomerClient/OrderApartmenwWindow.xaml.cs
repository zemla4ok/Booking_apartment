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
using System.Windows.Shapes;
using WpfApplication2;

namespace CustomerClient
{
    /// <summary>
    /// Interaction logic for OrderApartmenwWindow.xaml
    /// </summary>
    public partial class OrderApartmenwWindow : Window
    {
        private bool flag = false;
        Apartment currApartment = new Apartment();

        public OrderApartmenwWindow(string apartm)
        {
            InitializeComponent();
            this.currApartment = JsonConvert.DeserializeObject<Apartment>(apartm);
            this.Info.Text = this.currApartment.Hotel + " " + this.currApartment.Stars
                + " Stars hotel in " + this.currApartment.City;

            this.ArrivingDate.DisplayDateStart = DateTime.Today;
            this.ArrivingDate.SelectedDate = DateTime.Today;
            this.EvictionDate.DisplayDateStart = DateTime.Today.AddDays(1);
            this.EvictionDate.SelectedDate = DateTime.Today.AddDays(1);

            this.GetSum();
            this.flag = true;
            if (this.currApartment.FreePlaces == 0)
                this.AcBook.IsEnabled = false;
        }

        private void GetSum()
        {
            using (SqlConnection cn = Connector.GetConnection())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("GetSumForOrder", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter city = new SqlParameter();
                city.ParameterName = "@city";
                city.Value = this.currApartment.City;
                cmd.Parameters.Add(city);

                SqlParameter hotel = new SqlParameter();
                hotel.ParameterName = "@hotel";
                hotel.Value = this.currApartment.Hotel;
                cmd.Parameters.Add(hotel);

                SqlParameter is_doseage = new SqlParameter();
                is_doseage.ParameterName = "@is_doseage";
                is_doseage.Value = this.IsDoseage.IsChecked;
                cmd.Parameters.Add(is_doseage);

                SqlParameter places = new SqlParameter();
                places.ParameterName = "@places";
                places.Value = this.Places.Text;
                cmd.Parameters.Add(places);

                SqlParameter apart_num = new SqlParameter();
                apart_num.ParameterName = "@apart_num";
                apart_num.Value = this.currApartment.ApartNumber;
                cmd.Parameters.Add(apart_num);

                SqlParameter cost = new SqlParameter();
                cost.ParameterName = "@cost";
                cost.Value = this.currApartment.Cost;
                cmd.Parameters.Add(cost);

                SqlParameter is_early = new SqlParameter();
                is_early.ParameterName = "@is_early";
                is_early.Value = this.IsEarly.IsChecked;
                cmd.Parameters.Add(is_early);

                SqlParameter evic_date = new SqlParameter();
                evic_date.ParameterName = "@evic_date";
                evic_date.Value = this.EvictionDate.SelectedDate.ToString().Substring(0, 10);
                cmd.Parameters.Add(evic_date);

                SqlParameter arr_date = new SqlParameter();
                arr_date.ParameterName = "@arr_date";
                arr_date.Value = this.ArrivingDate.SelectedDate.ToString().Substring(0, 10);
                cmd.Parameters.Add(arr_date);

                SqlParameter rc = new SqlParameter();
                rc.ParameterName = "@rc";
                rc.Size = 20;
                rc.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(rc);

                cmd.ExecuteNonQuery();
                this.Sum.Text = cmd.Parameters["@rc"].Value.ToString();

                cn.Close();
            }
        }

        private void OnArrivingDateChange(object sender, SelectionChangedEventArgs e)
        {
            if (this.flag)
            {
                this.GetSum();
                if (Convert.ToInt32(this.Sum.Text) < 0)
                    this.AcBook.IsEnabled = false;
                else
                    this.AcBook.IsEnabled = true;
            }
            if (this.currApartment.FreePlaces == 0)
                this.AcBook.IsEnabled = false;
        }

        private void OnEvictionDateChange(object sender, SelectionChangedEventArgs e)
        {
            if (this.flag)
            {
                this.GetSum();
                if (Convert.ToInt32(this.Sum.Text) < 0)
                    this.AcBook.IsEnabled = false;
                else
                    this.AcBook.IsEnabled = true;
            }
            if (this.currApartment.FreePlaces == 0)
                this.AcBook.IsEnabled = false;
        }

        private void OnIsDoseageChangeg(object sender, RoutedEventArgs e)
        {
            this.GetSum();
        }

        private void OnPlacesChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.flag)
            {
                this.GetSum();
                if (Convert.ToInt32(this.Places.Text) > this.currApartment.FreePlaces)
                    this.AcBook.IsEnabled = false;
                else
                    this.AcBook.IsEnabled = true;
            }
        }

        private void OnIsEarlyChanged(object sender, RoutedEventArgs e)
        {
            this.GetSum();
        }

        private void AcceptBooking(object sender, RoutedEventArgs e)
        {
            if (Validator.ValidTextBoxes(this.Name.Text, this.Surname.Text, this.PassNumb.Text))
            {
                try
                {
                    using (SqlConnection cn = Connector.GetConnection())
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("BookApartments", cn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter city = new SqlParameter();
                        city.ParameterName = "@city";
                        city.Value = this.currApartment.City;
                        cmd.Parameters.Add(city);

                        SqlParameter hotel = new SqlParameter();
                        hotel.ParameterName = "@hotel";
                        hotel.Value = this.currApartment.Hotel;
                        cmd.Parameters.Add(hotel);

                        SqlParameter apart_num = new SqlParameter();
                        apart_num.ParameterName = "@apart_num";
                        apart_num.Value = this.currApartment.ApartNumber;
                        cmd.Parameters.Add(apart_num);

                        SqlParameter free_places = new SqlParameter();
                        free_places.ParameterName = "@free_pl";
                        free_places.Value = this.currApartment.FreePlaces;
                        cmd.Parameters.Add(free_places);

                        SqlParameter user_name = new SqlParameter();
                        user_name.ParameterName = "@user_name";
                        user_name.Value = this.Name.Text;
                        cmd.Parameters.Add(user_name);

                        SqlParameter user_surname = new SqlParameter();
                        user_surname.ParameterName = "@user_surname";
                        user_surname.Value = this.Surname.Text;
                        cmd.Parameters.Add(user_surname);

                        SqlParameter passp_num = new SqlParameter();
                        passp_num.ParameterName = "@passport_num";
                        passp_num.Value = this.PassNumb.Text;
                        cmd.Parameters.Add(passp_num);

                        SqlParameter cost = new SqlParameter();
                        cost.ParameterName = "@cost";
                        cost.Value = this.currApartment.Cost;
                        cmd.Parameters.Add(cost);

                        SqlParameter arr_date = new SqlParameter();
                        arr_date.ParameterName = "@arr_date";
                        arr_date.Value = this.ArrivingDate.SelectedDate.ToString().Substring(0, 10);
                        cmd.Parameters.Add(arr_date);

                        SqlParameter evic_date = new SqlParameter();
                        evic_date.ParameterName = "@evic_date";
                        evic_date.Value = this.EvictionDate.SelectedDate.ToString().Substring(0, 10);
                        cmd.Parameters.Add(evic_date);

                        SqlParameter is_early = new SqlParameter();
                        is_early.ParameterName = "@is_early";
                        is_early.Value = this.IsEarly.IsChecked;
                        cmd.Parameters.Add(is_early);

                        SqlParameter is_doseage = new SqlParameter();
                        is_doseage.ParameterName = "@is_doseeage";
                        is_doseage.Value = this.IsDoseage.IsChecked;
                        cmd.Parameters.Add(is_doseage);

                        SqlParameter res_pl = new SqlParameter();
                        res_pl.ParameterName = "@reserv_places";
                        res_pl.Value = this.Places.Text;
                        cmd.Parameters.Add(res_pl);

                        SqlParameter rc = new SqlParameter();
                        rc.ParameterName = "@rc";
                        rc.SqlDbType = System.Data.SqlDbType.Bit;
                        rc.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(rc);

                        cmd.ExecuteNonQuery();

                        if ((bool)cmd.Parameters["@rc"].Value)
                        {
                            MessageBox.Show("Booking is succesfull");
                        }
                        else
                        { 
                            MessageBox.Show("Error");
                        }


                        cn.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Input data about yourself");
            }
        }
    }
}
