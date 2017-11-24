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
            try
            {
                if(Validator.ValidTextBoxes(this.CloseDate.Text))
                {

                }
                else
                {
                    MessageBox.Show("Input Close date");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
