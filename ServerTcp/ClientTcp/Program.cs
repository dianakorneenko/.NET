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
            #region TCP
            // ip адрес (стандартный localhost)
            const string id = "127.0.0.1";
            // порт
            const int port = 8080;
            
            // end point
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(id), port);
            // socket
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            // создаем сообщение
            Console.WriteLine("Введите сообщение: ");
            var messenge = Console.ReadLine();
            var data = Encoding.UTF8.GetBytes(messenge);

            // сделаем подключение для нашего сокета
            tcpSocket.Connect(tcpEndPoint);
            // отправляем запрос
            tcpSocket.Send(data);

            // получение ответа
            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                // установили нужный размер для сообщения
                size = tcpSocket.Receive(buffer);
                // добавляем наши раскодированные сообщения из буфера в data
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (tcpSocket.Available > 0);

            // выведем ответ
            Console.WriteLine(answer.ToString());

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            Console.ReadLine();
            #endregion

            #region UDP
            /*
            // ip адрес (стандартный localhost)
            const string id = "127.0.0.1";
            // порт
            const int port = 8082;

            // end point
            var udpEndPoint = new IPEndPoint(IPAddress.Parse(id), port);
            // socket
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);

            //отправка сообщения
            while (true)
            {
                Console.WriteLine("Введите сообщение: ");
                var messege = Console.ReadLine();
                // отпрвляем уже на конкретный порт
                var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081); 
                udpSocket.SendTo(Encoding.UTF8.GetBytes(messege), serverEndPoint);

                //читаем ответ

                // создаем буфер, задаем max кол-во байт 256
                var buffer = new byte[256];
                // реальное кол-во байт
                var size = 0;
                var data = new StringBuilder();
                // сохраним адрес клиента, мы не знаем кто точно, то есть любой чел
                EndPoint senderEndpoint = new IPEndPoint(IPAddress.Any, 0);

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndpoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                } while (udpSocket.Available > 0);

                Console.WriteLine(data);

                Console.ReadLine();
            }
            */
            #endregion
        }
    }
}
