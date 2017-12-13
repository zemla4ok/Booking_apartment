using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input_data
{
    static class inpData
    {
        static public void insCities()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-M13O155;Initial Catalog=BookingApartment;Integrated Security=True"))
                {
                    cn.Open();
                    string insert = string.Format("INSERT INTO CITY" + "(NAME) VALUES(@name)");
                    string city = "city";
                    for (int i = 1; i <= 50; i++)
                    {
                        SqlCommand cmd = new SqlCommand(insert, cn);
                        cmd.Parameters.AddWithValue("@name", city + i);
                        cmd.ExecuteNonQuery();
                    }

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void insHotels()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-M13O155;Initial Catalog=BookingApartment;Integrated Security=True"))
                {
                    cn.Open();
                    string insert = string.Format("INSERT INTO HOTELS" + "(NAME, STARS, CITY_ID, HOTEL_PASSWORD) VALUES(@name, @stars, @city, @pass)");
                    string hotel = "hotel";
                    int counter = 1;
                    Random random = new Random();
                    for (int i = 1; i <= 50; i++)
                    {                        
                        for (int j = 1; j <= 20; j++)
                        {
                            SqlCommand cmd = new SqlCommand(insert, cn);
                            cmd.Parameters.AddWithValue("@name", hotel + counter);
                            cmd.Parameters.AddWithValue("@stars", (random.Next() % 5) + 1);
                            cmd.Parameters.AddWithValue("@city", i);
                            cmd.Parameters.AddWithValue("@pass", 1111);
                            cmd.ExecuteNonQuery();
                            counter++;
                        }
                    }

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void ins10kApartments()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-M13O155;Initial Catalog=BookingApartment;Integrated Security=True"))
                {
                    cn.Open();
                    string insert = string.Format("INSERT INTO APARTMENTS" + "(CURRENT_COST, PLACES, FREE_PLACES, HOTEL_ID, APARTMENTS_NUM, IS_CLOSE) VALUES(@cost, @places, @free, @hotel, @num, @close)");
                    int counter = 1;
                    int places;
                    Random random = new Random();
                    for (int i = 1; i <= 5; i++)
                    {
                        for (int j = 1; j <= 20; j++)
                        {
                            for (int k = 1; k <= 100; k++)
                            {
                                places = random.Next() % 5 + 1;
                                SqlCommand cmd = new SqlCommand(insert, cn);
                                cmd.Parameters.AddWithValue("@cost", (random.Next() % 150) + 1);
                                cmd.Parameters.AddWithValue("@places", places);
                                cmd.Parameters.AddWithValue("@free", random.Next() % places +1);
                                cmd.Parameters.AddWithValue("@hotel", counter);
                                cmd.Parameters.AddWithValue("@num", k);
                                cmd.Parameters.AddWithValue("@close", random.Next() % 2);

                                cmd.ExecuteNonQuery();
                            }
                            counter++;
                        }
                    }

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void ins100kApartments()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-M13O155;Initial Catalog=BookingApartment;Integrated Security=True"))
                {
                    cn.Open();
                    string insert = string.Format("INSERT INTO APARTMENTS" + "(CURRENT_COST, PLACES, FREE_PLACES, HOTEL_ID, APARTMENTS_NUM, IS_CLOSE) VALUES(@cost, @places, @free, @hotel, @num, @close)");
                    int counter = 1;
                    int places;
                    Random random = new Random();
                    for (int i = 1; i <= 50; i++)
                    {
                        for (int j = 1; j <= 20; j++)
                        {
                            for (int k = 1; k <= 100; k++)
                            {
                                places = random.Next() % 5 + 1;
                                SqlCommand cmd = new SqlCommand(insert, cn);
                                cmd.Parameters.AddWithValue("@cost", (random.Next() % 150) + 1);
                                cmd.Parameters.AddWithValue("@places", places);
                                cmd.Parameters.AddWithValue("@free", random.Next() % places + 1);
                                cmd.Parameters.AddWithValue("@hotel", counter);
                                cmd.Parameters.AddWithValue("@num", k);
                                cmd.Parameters.AddWithValue("@close", random.Next() % 2);

                                cmd.ExecuteNonQuery();
                            }
                            counter++;
                        }
                    }

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
