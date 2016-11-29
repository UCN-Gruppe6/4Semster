using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ChatClient
{
    public partial class PublicRoom : Form
    {
        #region Fields and Properties

        private string userName = "Unknown";

        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;

        private delegate void UpdateLogCallback(string strMessage);
        private delegate void CloseConnectionCallback(string strReason);

        private Thread thrMessaging;
        private IPAddress ipAddress;
        private bool Connected;

        #endregion

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(Connected == false)
            {
                InitializeConnection();
            }
            else
            {
                CloseConnection("Disconnected at user's request.");
            }
        }

        #region Open and Close Connection

        // Laver forbenlsen til severn via IP addrass og Porten.
        private void InitializeConnection()
        {
            ipAddress = IPAddress.Parse(textIPAddrass.Text);
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddress, 8000);

            Connected = true;
            userName = textUserName.Text;

            // Aktiver og Deaktiver de rigtige felter og knapper.
            textIPAddrass.Enabled = false;
            textUserName.Enabled = false;
            textMegsse.Enabled = true;
            buttonSend.Enabled = true;
            buttonConnect.Text = "Disconnect";

            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(textUserName.Text);
            swSender.Flush();

            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();
        }

        // Metode der lukker alle åbne forbendlser.
        private void CloseConnection(string Reason)
        {
            // Aktiver og Deaktiver de rigtige felter og knapper.
            textChatLog.AppendText(Reason + "\r\n");
            textIPAddrass.Enabled = true;
            textUserName.Enabled = true;
            textMegsse.Enabled = false;
            buttonSend.Enabled = false;
            buttonConnect.Text = "Connect";

            // Lukker alle forbendlser.
            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
            thrMessaging.Abort();
            srReceiver.Dispose();
            swSender.Dispose();
        }

        #endregion

        #region Send and Receive Message

        private void ReceiveMessages()
        {
            srReceiver = new StreamReader(tcpServer.GetStream());
            string ConResponse = srReceiver.ReadLine();

            if (ConResponse[0] == '1')
            {
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
            }
            else
            {
                string Reason = "Not Connected: ";
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                return;
            }

            while (Connected == true)
            {
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
            }
        }

        private void UpdateLog(string strMessage)
        {
            textChatLog.AppendText(strMessage + "\r\n");
        }

        // Sender beskeden ved at have kliket på knappen "Send". 
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        // Sender beskeden ved at have kliket enter.
        private void textMegsse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendMessage();
            }
        }

        // Den besked der bliver sendt.
        private void SendMessage()
        {
            if(textMegsse.Lines.Length >= 1)
            {
                swSender.WriteLine(textMegsse.Text);
                swSender.Flush();
                textMegsse.Lines = null;
            }
            textMegsse.Text = "";
        }

        #endregion

        public PublicRoom()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
        }

        // Bliver brugt hvis brugeren lukker programmet ned uden at have trykket "Disconnect".
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected == true)
            {
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
                thrMessaging.Abort();
                srReceiver.Dispose();
                swSender.Dispose();
            }
        }
    }
}
