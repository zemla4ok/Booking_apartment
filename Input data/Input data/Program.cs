using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input_data
{
    class Program
    {
        static void Main(string[] args)
        {
            //add cities
            inpData.insCities();

            //add hotels
            inpData.insHotels();


            //add 10k apartments
            //inpData.ins10kApartments();

            //add 100k apartments
            inpData.ins100kApartments();
        }

       
    }
}
