using Newtonsoft.Json;
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

namespace CustomerClient
{
    /// <summary>
    /// Interaction logic for OrderApartmenwWindow.xaml
    /// </summary>
    public partial class OrderApartmenwWindow : Window
    {
        Apartment currApartment = new Apartment();

        public OrderApartmenwWindow(string apartm)
        {
            InitializeComponent();
            this.currApartment = JsonConvert.DeserializeObject<Apartment>(apartm);
            this.Info.Text = this.currApartment.Hotel + " " + this.currApartment.Stars 
                + " Stars hotel in " + this.currApartment.City;

        }


    }
}
