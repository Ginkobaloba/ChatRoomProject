using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();

        static void Main(string[] args) 
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any,12000);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;
            int result = 0;

            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            counter = 0;
            while ((true))
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();

                byte[] bytesFrom = new byte[65536];
                string dataFromClient = null;

                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("m$m"));
                clientsList.Add(clientSocket, dataFromClient);
                Broadcast(dataFromClient + " Joined ", dataFromClient, true);
                Console.WriteLine(dataFromClient + " Joined chat room ");
                HandleClient client = new HandleClient();
                client.startClient(clientSocket, dataFromClient, clientsList);
            }
        }

        public static void Broadcast(string message,  string clientName, bool isHidden, string listUserName = null)
        {
            foreach (DictionaryEntry key in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)key.Key;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;
                string broadCastSystem = null;
                string broadCastUser = null;

                if (listUserName == null)
                {
                    broadCastSystem = message + "m$m";
                    broadCastUser = clientName + " says : " + message + "m$m";
                }
                else
                {
                    broadCastSystem = message + "m$m" + listUserName + "u$u";
                    broadCastUser = clientName + " says : " + message + "m$m" + listUserName + "u$u";
                }

                if (isHidden == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(broadCastSystem);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(broadCastUser);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        } 
    }


    public class HandleClient
    {
        TcpClient clientSocket;
        string clientName;
        Hashtable clientsList;

        public void startClient(TcpClient inClientSocket, string clientName, Hashtable clientList)
        {
            this.clientSocket = inClientSocket;
            this.clientName = clientName;
            this.clientsList = clientList;
            Thread Thread = new Thread(doChat);
            Thread.Start();
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[65536];
            string dataFromClient = null;
            string dataFromClientUnedited = null;
            string userNamesFromServer = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            int length;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClientUnedited = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClientUnedited.Substring(0, dataFromClientUnedited.IndexOf("m$m"));
                    Console.WriteLine("From client - " + clientName + " : " + dataFromClient);
                    rCount = Convert.ToString(requestCount);
                    userNamesFromServer = GetConnectedUsers();
                    Program.Broadcast(dataFromClient, clientName, false, userNamesFromServer);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private string GetConnectedUsers()
        {
            List<string> users = new List<string>();
            string listOfUsers = null;


            foreach (DictionaryEntry Key in clientsList)
            {
                users.Add(Key.Value.ToString());
            }

            if (users.Count == 1)
                {
                listOfUsers = users[0];
                 }
            else
            {
                for (int i = 0; i < users.Count - 1; i++)
                {
                    listOfUsers = listOfUsers + users[i]+ "/r/n";
                }
                listOfUsers = listOfUsers + users[users.Count - 1];
            }
                return listOfUsers;
        }
    }
}