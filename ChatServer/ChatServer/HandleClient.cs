using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer
{
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
            byte[] bytesFrom = new byte[65536];
            string broadCastDataFromClient = null;
            string messageDataFromClient = null;
            string dataFromClientUnedited = null;
            List<string> broadCastList;

            while (true)
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClientUnedited = Encoding.ASCII.GetString(bytesFrom);
                    messageDataFromClient = dataFromClientUnedited.Substring(0, dataFromClientUnedited.IndexOf("m$m"));
                    Console.WriteLine("From client - " + clientName + " : " + messageDataFromClient);
                    broadCastDataFromClient = dataFromClientUnedited.Substring(dataFromClientUnedited.IndexOf("m$m") + 3, dataFromClientUnedited.IndexOf("b$c")-dataFromClientUnedited.IndexOf("m$m")-3);
                    broadCastList = CreateBroadCastList(broadCastDataFromClient);
                    Program.Broadcast(messageDataFromClient, clientName, false, broadCastList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private List<string> CreateBroadCastList(string broadCastData)
        {
            string oneUsersData = null;
            int length;
            List<string> broadCastList = new List<string>();

            while (broadCastData.IndexOf("b/c") > 0)
            {
                length = broadCastData.Length;
                length = length - broadCastData.IndexOf("b/c");
                oneUsersData = broadCastData.Substring(0, broadCastData.IndexOf("b/c"));
                oneUsersData = oneUsersData.ToUpper();
                broadCastList.Add(oneUsersData);
                broadCastData = broadCastData.Substring(broadCastData.IndexOf("b/c") + 3, (broadCastData.Length - (broadCastData.IndexOf("b/c") + 3)));

            }
            return broadCastList;
        }
    }
}
