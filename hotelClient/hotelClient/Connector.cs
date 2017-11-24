using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelClient
{
    static class Connector
    {
        const string path = @"Data Source=DESKTOP-M13O155;Initial Catalog=BookingApartment;User Id=HOTEL_ADMINISTRATOR; Password=1111";

        static public SqlConnection GetConnection()
        {
            return new SqlConnection(path);
        }
    }
}
