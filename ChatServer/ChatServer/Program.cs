using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();

        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 12000);
            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            while ((true))
            {
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

        public static void Broadcast(string message, string clientName, bool isHidden, List<string> broadCastList = null)
        {
            TcpClient broadcastSocket;
            NetworkStream broadcastStream;
            Queue Messages = new Queue();
            Byte[] broadcastBytes = null;
            string broadCastMessage = null;
            string listOfUsers = null;

            listOfUsers = GetConnectedUsers();

            if (broadCastList == null)
            {
                foreach (DictionaryEntry client in clientsList)
                {
                    broadcastSocket = (TcpClient)client.Key;
                    broadcastStream = broadcastSocket.GetStream();
                    if (isHidden == true)
                    {
                        broadCastMessage = message + "m$m" + listOfUsers + "u$u";
                        broadcastBytes = Encoding.ASCII.GetBytes(broadCastMessage);
                        broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                        broadcastStream.Flush();
                    }
                    else
                    {
                        broadCastMessage = clientName + " says : " + message + "m$m" + listOfUsers + "u$u";
                        broadcastBytes = Encoding.ASCII.GetBytes(broadCastMessage);
                        broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                        broadcastStream.Flush();
                    }
                }
            }
            else
            {
                foreach (DictionaryEntry client in clientsList)
                {
                    for (int i = 0; i < broadCastList.Count; i++)

                        if (client.Value.ToString().ToUpper() == broadCastList[i])
                        {
                            broadcastSocket = (TcpClient)client.Key;
                            broadcastStream = broadcastSocket.GetStream();

                            if (isHidden == true)
                            {
                                broadCastMessage = message + "m$m" + listOfUsers + "u$u";
                                broadcastBytes = Encoding.ASCII.GetBytes(broadCastMessage);
                                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                                broadcastStream.Flush();
                            }
                            else
                            {
                                broadCastMessage = clientName + " says : " + message + "m$m" + listOfUsers + "u$u";
                                broadcastBytes = Encoding.ASCII.GetBytes(broadCastMessage);
                                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                                broadcastStream.Flush();
                            }
                        }
                }
            }
        }



        public static string GetConnectedUsers()
        {
            List<string> users = new List<string>();
            string listOfUsers = null;


            foreach (DictionaryEntry Key in clientsList)
            {
                users.Add(Key.Value.ToString());
            }

            if (users.Count == 1)
            {
                listOfUsers = users[0] + "/u/";
            }
            else
            {
                for (int i = 0; i < users.Count - 1; i++)
                {
                    listOfUsers = listOfUsers + users[i] + "/u/";
                }
                listOfUsers = listOfUsers + users[users.Count - 1] + "/u/";
            }
            return listOfUsers;
        }

        private void SendMessageQueue()
        {


        }
    }
    }
