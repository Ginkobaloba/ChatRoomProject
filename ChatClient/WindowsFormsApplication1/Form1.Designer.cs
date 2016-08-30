namespace ChatClient
{
    partial class frmChatClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtChatWindow = new System.Windows.Forms.TextBox();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.btnConnectToServer = new System.Windows.Forms.Button();
            this.lblSetUserName = new System.Windows.Forms.Label();
            this.lblUserNames = new System.Windows.Forms.Label();
            this.txtUsers = new System.Windows.Forms.TextBox();
            this.lblConnectedUsers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtChatWindow
            // 
            this.txtChatWindow.Location = new System.Drawing.Point(20, 103);
            this.txtChatWindow.Multiline = true;
            this.txtChatWindow.Name = "txtChatWindow";
            this.txtChatWindow.Size = new System.Drawing.Size(649, 499);
            this.txtChatWindow.TabIndex = 2;
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.Location = new System.Drawing.Point(20, 606);
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(649, 26);
            this.txtSendMessage.TabIndex = 1;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(313, 33);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(356, 26);
            this.txtUserName.TabIndex = 0;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(465, 638);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(204, 39);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // btnConnectToServer
            // 
            this.btnConnectToServer.Location = new System.Drawing.Point(397, 65);
            this.btnConnectToServer.Name = "btnConnectToServer";
            this.btnConnectToServer.Size = new System.Drawing.Size(272, 35);
            this.btnConnectToServer.TabIndex = 4;
            this.btnConnectToServer.Text = "Connect to Server";
            this.btnConnectToServer.UseVisualStyleBackColor = true;
            this.btnConnectToServer.Click += new System.EventHandler(this.btnConnectToServer_Click);
            // 
            // lblSetUserName
            // 
            this.lblSetUserName.AutoSize = true;
            this.lblSetUserName.Location = new System.Drawing.Point(186, 33);
            this.lblSetUserName.Name = "lblSetUserName";
            this.lblSetUserName.Size = new System.Drawing.Size(118, 20);
            this.lblSetUserName.TabIndex = 6;
            this.lblSetUserName.Text = "Set User Name";
            // 
            // lblUserNames
            // 
            this.lblUserNames.AutoSize = true;
            this.lblUserNames.Location = new System.Drawing.Point(687, 36);
            this.lblUserNames.Name = "lblUserNames";
            this.lblUserNames.Size = new System.Drawing.Size(0, 20);
            this.lblUserNames.TabIndex = 7;
            // 
            // txtUsers
            // 
            this.txtUsers.Location = new System.Drawing.Point(684, 65);
            this.txtUsers.Multiline = true;
            this.txtUsers.Name = "txtUsers";
            this.txtUsers.Size = new System.Drawing.Size(261, 607);
            this.txtUsers.TabIndex = 8;
            // 
            // lblConnectedUsers
            // 
            this.lblConnectedUsers.AutoSize = true;
            this.lblConnectedUsers.Location = new System.Drawing.Point(752, 33);
            this.lblConnectedUsers.Name = "lblConnectedUsers";
            this.lblConnectedUsers.Size = new System.Drawing.Size(133, 20);
            this.lblConnectedUsers.TabIndex = 9;
            this.lblConnectedUsers.Text = "Connected Users";
            this.lblConnectedUsers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmChatClient
            // 
            this.ClientSize = new System.Drawing.Size(957, 747);
            this.Controls.Add(this.lblConnectedUsers);
            this.Controls.Add(this.txtUsers);
            this.Controls.Add(this.lblUserNames);
            this.Controls.Add(this.lblSetUserName);
            this.Controls.Add(this.btnConnectToServer);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.txtChatWindow);
            this.Name = "frmChatClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChatWindow;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnConnectToServer;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Label lblSetUserName;
        private System.Windows.Forms.Label lblUserNames;
        private System.Windows.Forms.TextBox txtUsers;
        private System.Windows.Forms.Label lblConnectedUsers;
    }
}

