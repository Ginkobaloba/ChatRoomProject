using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ChatClient
{
    public partial class frmChatClient : Form
    {
        TcpClient MessagesSocket = new TcpClient();
        NetworkStream MessagesStream = default(NetworkStream);
        string returnedMessageData = null;
        string returnedUserData = null;
        public frmChatClient()
        {
            InitializeComponent();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            byte[] outStream = Encoding.ASCII.GetBytes(txtSendMessage.Text + "m$m");
            MessagesStream.Write(outStream, 0, outStream.Length);
            MessagesStream.Flush();
            txtSendMessage.Clear();
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            LoopConnect();
        }

        private void getMessage()
        {
            string unEditedDataFromServer = null;
            string messageData = null;
            int length;
            int lengthOfPacketCode = 3;
            while (MessagesSocket.Connected)
            {
                MessagesStream = MessagesSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[65536];
                buffSize = MessagesSocket.ReceiveBufferSize;
                MessagesStream.Read(inStream, 0, inStream.Length);
                unEditedDataFromServer = Encoding.ASCII.GetString(inStream);
                messageData = unEditedDataFromServer.Substring(0, unEditedDataFromServer.IndexOf("m$m"));
                returnedMessageData = "" + messageData;

                if (unEditedDataFromServer.IndexOf("u$u") >= 0)
                {
                    length = unEditedDataFromServer.IndexOf("u$u");
                    length = length - unEditedDataFromServer.IndexOf("m$m");
                    returnedUserData = unEditedDataFromServer.Substring(unEditedDataFromServer.IndexOf("m$m") + lengthOfPacketCode, length - lengthOfPacketCode);
                }

                UpdateGUI();

            }
        }

        private void UpdateGUI()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(UpdateGUI));
            else if (this.InvokeRequired!=true && returnedUserData == null)
                txtChatWindow.Text = txtChatWindow.Text + Environment.NewLine + " >> " + returnedMessageData;
           else 
            {
                txtChatWindow.Text = txtChatWindow.Text + Environment.NewLine + " >> " + returnedMessageData;
                txtConnectedUsers.AppendText(returnedUserData);
                txtConnectedUsers.Text =  returnedUserData;
            }
        }
        private void LoopConnect()
        {
            int attempts = 0;

            while (!MessagesSocket.Connected)
            {
                try
                {
                    attempts++;
                    MessagesSocket.Connect("10.2.20.21", 12000);
                }
                catch (SocketException)
                {
                    txtChatWindow.Clear();
                    txtChatWindow.AppendText("Connection attempts: " + attempts.ToString());
                }
            }
            txtChatWindow.Clear();
            returnedMessageData = "Conected to Chat Server ...";
            UpdateGUI();
            MessagesStream = MessagesSocket.GetStream();
            btnConnectToServer.Visible = false;
            btnSendMessage.Visible = true;
            txtSendMessage.Visible = true;

            byte[] outStreamMessage = Encoding.ASCII.GetBytes(txtUserName.Text + "m$m");
            MessagesStream.Write(outStreamMessage, 0, outStreamMessage.Length);
            MessagesStream.Flush();
            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }

    }
}
