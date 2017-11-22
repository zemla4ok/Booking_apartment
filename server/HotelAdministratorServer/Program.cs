using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelAdministratorServer
{
    class Program
    {
        const int port = 8888;
        const string ip = "127.0.0.1";
        static TcpListener listener;

        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(ip), port);
                listener.Start();
                Console.WriteLine("Server is started");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);

                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
