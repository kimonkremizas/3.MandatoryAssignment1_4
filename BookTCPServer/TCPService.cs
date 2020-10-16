using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using BookLibrary;
using System.Text;
using System.Text.Json;

namespace BookTCPServer
{
    public class TCPService
    {
        private TcpClient connectionSocket;

        public TCPService(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        private Book GetBook(string isbn13)
        {
            return BookList.bookList.FirstOrDefault(b => b.Isbn13 == isbn13);
        }

        private List<Book> GetBooks()
        {
            return BookList.bookList;
        }

        private void SaveBook(Book newBook)
        {
            BookList.bookList.Add(newBook);
        }

        internal void StartService()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            sw.Write("Valid requests are: GetAll, Get and Save");
            string clientMessage = sr.ReadLine();
            string serverResponse = "";
            while (!string.IsNullOrEmpty(clientMessage))
            {
                string[] request = clientMessage.Split("|");

                switch (request[0])
                {
                    case "GetAll":
                        serverResponse = JsonSerializer.Serialize(GetBooks());
                        break;
                    case "Get":
                        serverResponse = JsonSerializer.Serialize(GetBook(request[1]));
                        break;
                    case "Save":
                        SaveBook(JsonSerializer.Deserialize<Book>(request[1]));
                        break;
                    default:
                        serverResponse = "Request does not exist.";
                        break;
                }

                Console.WriteLine("Client Request: " + request[0]);
                sw.WriteLine(serverResponse);
                clientMessage = sr.ReadLine();

            }
            ns.Close();
            connectionSocket.Close();
        }
    }
}
