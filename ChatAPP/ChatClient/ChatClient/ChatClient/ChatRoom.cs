using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ChatClient
{
    public partial class ChatRoom : Form
    {
        #region Fields and Properties

        // Sætter brugernavnet til at man er ukent.
        private string userName = "Unknown";

        // Dette er det er bliver brugt til at sende data frem og tilbage
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private NetworkStream netStream;
        private SslStream ssl;

        // Sevren man er forbundet til.
        private TcpClient tcpServer;

        // Bliver brugt til at opdater chat vinduet.
        private delegate void UpdateLogCallback(string strMessage);
        private delegate void CloseConnectionCallback(string strReason);

        private Thread thrMessaging;
        private IPAddress ipAddress;
        private bool Connected; // holder styr på om vi er forbundet til severn.

        #endregion

        // Knappen "Connect". Når der bliver trtkket forbender man sig til den ip addrasse man har skrivet.
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
            // Forbindlse til severn og sørger for at forbendlsen er encrypted.
            ipAddress = IPAddress.Parse(textIPAddrass.Text);
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddress, 8000);

            netStream = tcpServer.GetStream();
            ssl = new SslStream(netStream, false, new RemoteCertificateValidationCallback(ValidateCert));
            ssl.AuthenticateAsClient("ChatSever");

            Connected = true;
            userName = textUserName.Text;

            // Aktiver og Deaktiver de rigtige felter og knapper.
            textIPAddrass.Enabled = false;
            textUserName.Enabled = false;
            textMegsse.Enabled = true;
            buttonSend.Enabled = true;
            buttonConnect.Text = "Disconnect";

            swSender = new StreamWriter(ssl, Encoding.UTF8);
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
            netStream.Close();
            thrMessaging.Abort();
            srReceiver.Dispose();
            swSender.Dispose();
            netStream.Dispose();
            ssl.Close();
            ssl.Dispose();
            tcpServer.Close();
        }

        #endregion

        #region Send and Receive Message

        private void ReceiveMessages()
        {
            netStream = tcpServer.GetStream();
            srReceiver = new StreamReader(ssl, Encoding.UTF8);

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
                if(srReceiver != null)
                {
                    this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
                }
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

        // Den besked der bliver sendt.
        private void SendMessage()
        {
            swSender = new StreamWriter(ssl, Encoding.UTF8);
            if(textMegsse.Lines.Length >= 1)
            {
                swSender.WriteLine(textMegsse.Text);
                swSender.Flush();
                textMegsse.Lines = null;
            }
            textMegsse.Text = "";
        }

        #endregion

        public ChatRoom()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
        }

        // Bliver brugt hvis brugeren lukker programmet ned uden at have trykket "Disconnect".
        // Trykker på "X"
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected == true)
            {
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
                netStream.Close();
                thrMessaging.Abort();
                srReceiver.Dispose();
                swSender.Dispose();
                netStream.Dispose();
                ssl.Close();
                ssl.Dispose();
            }
        }

        public static bool ValidateCert(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // De her linger kan udkometeres hvis man vil gøre sådan at man ikke tilader untrusted certificates. 
            //if (sslPolicyErrors == SslPolicyErrors.None)
            //    return true;
            //else
            //    return false;

            return true; // tillader untrusted certificates.
        }
    }
}
