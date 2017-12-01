using hotelClient;
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
            ApartmentList list = new ApartmentList();

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
                    list.AddApartment(ap);
                }
                this.Apartments.ItemsSource = list.GetApartments();
                cn.Close();
            }            
        }

        private void Apart_DoubleClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
