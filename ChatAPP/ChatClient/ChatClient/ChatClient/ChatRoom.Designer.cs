namespace ChatClient
{
    partial class ChatRoom
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.SeverIP = new System.Windows.Forms.Label();
            this.textIPAddrass = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.Label();
            this.textUserName = new System.Windows.Forms.TextBox();
            this.textChatLog = new System.Windows.Forms.TextBox();
            this.textMegsse = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.LableTo = new System.Windows.Forms.Label();
            this.textToUser = new System.Windows.Forms.TextBox();
            this.textPrivateMessage = new System.Windows.Forms.TextBox();
            this.buttonPrivate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.Location = new System.Drawing.Point(269, 33);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(86, 27);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // SeverIP
            // 
            this.SeverIP.AutoSize = true;
            this.SeverIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeverIP.Location = new System.Drawing.Point(12, 9);
            this.SeverIP.Name = "SeverIP";
            this.SeverIP.Size = new System.Drawing.Size(55, 15);
            this.SeverIP.TabIndex = 1;
            this.SeverIP.Text = "Sever IP:";
            this.SeverIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textIPAddrass
            // 
            this.textIPAddrass.Location = new System.Drawing.Point(92, 8);
            this.textIPAddrass.Name = "textIPAddrass";
            this.textIPAddrass.Size = new System.Drawing.Size(171, 20);
            this.textIPAddrass.TabIndex = 2;
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserName.Location = new System.Drawing.Point(12, 38);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(73, 15);
            this.UserName.TabIndex = 3;
            this.UserName.Text = "User Name:";
            // 
            // textUserName
            // 
            this.textUserName.Location = new System.Drawing.Point(92, 37);
            this.textUserName.Name = "textUserName";
            this.textUserName.Size = new System.Drawing.Size(171, 20);
            this.textUserName.TabIndex = 4;
            // 
            // textChatLog
            // 
            this.textChatLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textChatLog.Location = new System.Drawing.Point(15, 66);
            this.textChatLog.Multiline = true;
            this.textChatLog.Name = "textChatLog";
            this.textChatLog.ReadOnly = true;
            this.textChatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textChatLog.Size = new System.Drawing.Size(340, 277);
            this.textChatLog.TabIndex = 5;
            // 
            // textMegsse
            // 
            this.textMegsse.Enabled = false;
            this.textMegsse.Location = new System.Drawing.Point(15, 349);
            this.textMegsse.Name = "textMegsse";
            this.textMegsse.Size = new System.Drawing.Size(242, 20);
            this.textMegsse.TabIndex = 6;
            // 
            // buttonSend
            // 
            this.buttonSend.Enabled = false;
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.Location = new System.Drawing.Point(263, 349);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(92, 23);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // LableTo
            // 
            this.LableTo.AutoSize = true;
            this.LableTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LableTo.Location = new System.Drawing.Point(12, 380);
            this.LableTo.Name = "LableTo";
            this.LableTo.Size = new System.Drawing.Size(24, 15);
            this.LableTo.TabIndex = 8;
            this.LableTo.Text = "To:";
            // 
            // textToUser
            // 
            this.textToUser.Enabled = false;
            this.textToUser.Location = new System.Drawing.Point(42, 379);
            this.textToUser.Name = "textToUser";
            this.textToUser.Size = new System.Drawing.Size(56, 20);
            this.textToUser.TabIndex = 9;
            // 
            // textPrivateMessage
            // 
            this.textPrivateMessage.Enabled = false;
            this.textPrivateMessage.Location = new System.Drawing.Point(104, 379);
            this.textPrivateMessage.Name = "textPrivateMessage";
            this.textPrivateMessage.Size = new System.Drawing.Size(153, 20);
            this.textPrivateMessage.TabIndex = 11;
            // 
            // buttonPrivate
            // 
            this.buttonPrivate.Enabled = false;
            this.buttonPrivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrivate.Location = new System.Drawing.Point(263, 378);
            this.buttonPrivate.Name = "buttonPrivate";
            this.buttonPrivate.Size = new System.Drawing.Size(92, 24);
            this.buttonPrivate.TabIndex = 12;
            this.buttonPrivate.Text = "Send Prrivate";
            this.buttonPrivate.UseVisualStyleBackColor = true;
            this.buttonPrivate.Click += new System.EventHandler(this.buttonPrivate_Click);
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 409);
            this.Controls.Add(this.buttonPrivate);
            this.Controls.Add(this.textPrivateMessage);
            this.Controls.Add(this.textToUser);
            this.Controls.Add(this.LableTo);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textMegsse);
            this.Controls.Add(this.textChatLog);
            this.Controls.Add(this.textUserName);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.textIPAddrass);
            this.Controls.Add(this.SeverIP);
            this.Controls.Add(this.buttonConnect);
            this.Name = "ChatRoom";
            this.Text = "PublicRoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label SeverIP;
        private System.Windows.Forms.TextBox textIPAddrass;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.TextBox textUserName;
        private System.Windows.Forms.TextBox textChatLog;
        private System.Windows.Forms.TextBox textMegsse;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label LableTo;
        private System.Windows.Forms.TextBox textToUser;
        private System.Windows.Forms.TextBox textPrivateMessage;
        private System.Windows.Forms.Button buttonPrivate;
    }
}