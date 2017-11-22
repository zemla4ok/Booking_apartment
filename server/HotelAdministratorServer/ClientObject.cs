using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdministratorServer
{
    public class ClientObject
    {
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            this.client = tcpClient;
        }

        private string GetMessage(NetworkStream stream)
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }

        private void SendMessage(string message)
        {
            NetworkStream stream = null;
            stream = client.GetStream();
            byte[] data = new byte[64];
            data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void Process()
        {
            NetworkStream stream = null;
            stream = client.GetStream();
            while (true)
            {
               
            }
        }
    }
}