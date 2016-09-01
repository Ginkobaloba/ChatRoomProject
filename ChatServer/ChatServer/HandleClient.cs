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
                    Program.Broadcast(dataFromClient, clientName, false);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
