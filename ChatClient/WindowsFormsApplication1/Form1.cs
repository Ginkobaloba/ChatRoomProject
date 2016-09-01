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
        string oneUsersData = null;
        string broadCastList = null;
        int length;
        int lengthOfPacketCode = 3;

        public frmChatClient()
        {
            InitializeComponent();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string broadCastList;
            broadCastList = GetBroadcastList();
            byte[] outStream = Encoding.ASCII.GetBytes(txtSendMessage.Text + "m$m" + broadCastList + "b$c");
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
            int i;
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
            else if (this.InvokeRequired != true && returnedUserData == null)
                txtChatWindow.Text = txtChatWindow.Text + Environment.NewLine + " >> " + returnedMessageData;
            else
            {
                txtChatWindow.Text = txtChatWindow.Text + Environment.NewLine + " >> " + returnedMessageData;
                UpdateUserList();
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
                    MessagesSocket.Connect("10.2.20.20", 12000);
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
            txtUserName.Visible = false;
            lblSetUserName.Visible = false;
            lblConnectedUsers.Visible = true;
            btnSendMessage.Visible = true;
            txtSendMessage.Visible = true;

            byte[] outStreamMessage = Encoding.ASCII.GetBytes(txtUserName.Text + "m$m");
            MessagesStream.Write(outStreamMessage, 0, outStreamMessage.Length);
            MessagesStream.Flush();
            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }
        private void UpdateUserList()
        {
            while (chkListConnectedUsers.Items.Count != 0)
            {
                chkListConnectedUsers.Items.RemoveAt(0);
            }


            while (returnedUserData.IndexOf("/u/") > 0)
            {
                length = returnedUserData.Length;
                length = length - returnedUserData.IndexOf("/u/");
                oneUsersData = returnedUserData.Substring(0, returnedUserData.IndexOf("/u/"));
                chkListConnectedUsers.Items.Add(oneUsersData);
                returnedUserData = returnedUserData.Substring(returnedUserData.IndexOf("/u/") + lengthOfPacketCode, (returnedUserData.Length - (returnedUserData.IndexOf("/u/") + lengthOfPacketCode)));

            }

            for (int i = 0; i < chkListConnectedUsers.Items.Count; i++)
            {

                chkListConnectedUsers.SetItemChecked(i, true);
            }


        }
        public string GetBroadcastList()
        {


            foreach (string item in chkListConnectedUsers.CheckedItems)
                {
                broadCastList = broadCastList + item + "/bc/";
                }

                   return broadCastList;
        }

        private void chkListConnectedUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
   }

