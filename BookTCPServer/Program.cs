using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BookLibrary;

namespace BookTCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 4646);
            serverSocket.Start();
            Console.WriteLine("Server is waiting for clients...");

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Client Connected!");
                TCPService service = new TCPService(connectionSocket);

                Thread thread = new Thread(service.StartService);
                thread.Start();
            }
            serverSocket.Stop();
        }
    }
}
