using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 8080;
            const string ip = "127.0.0.1";

            var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(endPoint);
            
            Console.WriteLine("Введите сообщение: ");
            var message = Console.ReadLine();

            var buffer = Encoding.UTF8.GetBytes(message);
            socket.Send(buffer);

            Console.WriteLine("Ответ сервера: ");

            buffer = new byte[256];
            var bytes = 0;
            var builder = new StringBuilder();

            do
            {
                bytes = socket.Receive(buffer);
                builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            } while (socket.Available > 0);

            Console.WriteLine(builder.ToString());

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            
        }
    }
}
