using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 8080;
            const string ip = "127.0.0.1";

            var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(endPoint);
            listener.Listen(10);

            while (true)
            {
                Console.WriteLine("Ожидание подключений...");
                var client = listener.Accept();
                var builder = new StringBuilder();

                var buffer = new byte[256];
                var bytes = 0;

                do
                {
                    bytes = client.Receive(buffer);
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (client.Available > 0);

                Console.WriteLine("Сообщение пользователя: " + builder.ToString());

                string answer = "Ваше сообщение доставлено";
                buffer = Encoding.UTF8.GetBytes(answer);

                client.Send(buffer);


                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }



        }
    }
}
