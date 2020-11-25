using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP
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

            // нужно указать socket, что нужно слушать именно этот endpoint
            tcpSocket.Bind(tcpEndPoint);
            // запускаем socket на прослушивание, можно указать очередь, например, 5
            tcpSocket.Listen(5);

            // задаем процесс прослушивания, он должен быть бесконечным
            while (true)
            {
                // маленький слушатель, который создается для каждого клиента
                var listener = tcpSocket.Accept();
               
                // создаем буфер, задаем max кол-во байт 256
                var buffer = new byte[256];
                // реальное кол-во байт
                var size = 0;
                var data = new StringBuilder();

                // получаем сообщение
                // пока есть данные выполняем условие
                do
                {
                    // установили нужный размер для сообщения
                    size = listener.Receive(buffer);
                    // добавляем наши раскодированные сообщения из буфера в data
                    data.Append(Encoding.UTF8.GetString(buffer,0,size));
                }
                while (listener.Available > 0);

                Console.WriteLine(data);

                // обратный ответ
                listener.Send(Encoding.UTF8.GetBytes("Сообщение получено!"));

                // закрыть соединение listener
                // закрываем и у клиента и у сервера
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            #endregion

            #region UDP
            /*
            // ip адрес (стандартный localhost)
            const string id = "127.0.0.1";
            // порт
            const int port = 8081;

            // end point
            var udpEndPoint = new IPEndPoint(IPAddress.Parse(id), port);
            // socket
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            udpSocket.Bind(udpEndPoint);
            while (true)
            {
                // читаем запрос

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

                udpSocket.SendTo(Encoding.UTF8.GetBytes("Сообщение получено"), senderEndpoint);

                Console.WriteLine(data);        
            }

            // закрытие сокета
            */
            #endregion
        }
    }
}
