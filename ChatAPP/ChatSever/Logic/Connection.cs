using System;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Threading;
using System.IO;
using ChatSever.Logic;
using System.Security.Cryptography.X509Certificates;

namespace ChatSever.Logic
{
    // Denne klasse håntere alle forbinlser. Der er en instens af Connection for vær bruger der er forbundet.
    // Det er også denne klasse der sender beskeder fram og tilbage.  
    public class Connection
    {
        #region Fields and Properties

        TcpClient tcpClient;
        private Thread thrSender; // Den trød der sender data til clienten.

        private NetworkStream netStream;
        private SslStream ssl;
        private StreamReader srReceiver;
        private StreamWriter swSender;

        private string currUser;
        private string strResponse;

        #endregion

        // Stater en forbinlse og begynder at arkseteper bruger.
        // Laver en ny tcpClient. 
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            thrSender = new Thread(AcceptClient);
            thrSender.Start();
        }

        // Lukker forbinlsen. 
        private void CloseConnection()
        {
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
            netStream.Close();
            ssl.Close();
        }

        Program prog;
        // Arkseteper brugern og begynder at lytte efter trakfik og beskeder. 
        private void AcceptClient()
        { 
            Console.WriteLine("[{0}] New connection!", DateTime.Now);
            netStream = tcpClient.GetStream();
            ssl = new SslStream(netStream, false);
            ssl.AuthenticateAsServer(prog.cert, false, SslProtocols.Tls, true);
            Console.WriteLine("[{0}] Connection authenticated!", DateTime.Now);

            srReceiver = new StreamReader(ssl, Encoding.UTF8);
            swSender = new StreamWriter(ssl, Encoding.UTF8);

            currUser = srReceiver.ReadLine();

            if (currUser != "")
            {
                if(Server.users.Contains(currUser) == true) // Hvis brugernavnet allerede er taget. 
                {
                    swSender.WriteLine("0|This username already exists.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else if (currUser == "Administrator") // Hvis brugeren intaster "Administrator".
                {
                    swSender.WriteLine("0|This username is reserved.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else
                {
                    swSender.WriteLine("1");
                    swSender.Flush();

                    Server.AddUser(tcpClient, currUser);
                }
            }
            else
            {
                CloseConnection();
                return;
            }

            // Bliver ved med at vente på en besked fra clienten.
            // Sender også beskeder.
            try
            {

                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    if (strResponse == null)
                    {
                        Server.RemoveUser(tcpClient);
                    }
                    else
                    {
                        Server.SendMessage(currUser, strResponse);
                        Console.WriteLine("[{0}] " + currUser + " sent a message", DateTime.Now);
                    }
                }
            }
            catch
            {
                Server.RemoveUser(tcpClient);
            }
        }
    }
}
