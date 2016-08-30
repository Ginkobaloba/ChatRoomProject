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
        TcpClient UsersSocket = new TcpClient();
        NetworkStream MessagesStream = default(NetworkStream);
        NetworkStream UsersStream = default(NetworkStream);
        Random random = new Random();
        string readData = null;
        string userData = null;

        public frmChatClient()
        {
            InitializeComponent();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(txtSendMessage.Text + "$");
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
            while (MessagesSocket.Connected)
            {
                MessagesStream = MessagesSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[65536];
                buffSize = MessagesSocket.ReceiveBufferSize;
                MessagesStream.Read(inStream, 0, inStream.Length);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                readData = "" + returndata;
                msg();
            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                txtChatWindow.Text = txtChatWindow.Text + Environment.NewLine + " >> " + readData;
        }
        private void LoopConnect()
        {
            int attempts = 0;

            while (!MessagesSocket.Connected && !UsersSocket.Connected)
            {
                try
                {
                    attempts++;
                    MessagesSocket.Connect("10.2.20.11", 12000);
                    UsersSocket.Connect("10.2.20.11", 12000);
                }
                catch (SocketException)
                {
                    txtChatWindow.Clear();
                    txtChatWindow.AppendText("Connection attempts: " + attempts.ToString());
                }
            }
            txtChatWindow.Clear();
            readData = "Conected to Chat Server ...";
            msg();
            UsersStream = UsersSocket.GetStream();
            MessagesStream = MessagesSocket.GetStream();
            btnConnectToServer.Visible = false;

            byte[] outStreamMessage = Encoding.ASCII.GetBytes(txtUserName.Text + "$");
            byte[] outStreamWindow = Encoding.ASCII.GetBytes(random.Next(1, 25000).ToString());
            UsersStream.Write(outStreamWindow, 0, outStreamWindow.Length);
            MessagesStream.Write(outStreamMessage, 0, outStreamMessage.Length);
            UsersStream.Flush();
            MessagesStream.Flush();

            Thread ctThread = new Thread(getMessage);
            Thread GTAutoThread = new Thread(GetUserNames);
            ctThread.Start();
            GTAutoThread.Start();
               }
            private void GetUserNames()
                {
            while (UsersSocket.Connected)
            {
                UsersStream = UsersSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[65536];
                buffSize = UsersSocket.ReceiveBufferSize;
                UsersStream.Read(inStream, 0, inStream.Length);
                userData = Encoding.ASCII.GetString(inStream);
                setUserNames();
            }

          }
        private void setUserNames()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(setUserNames));
            else
                lblUserNames.Text = userData;
        }

        private void lstUsersConnected_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }
