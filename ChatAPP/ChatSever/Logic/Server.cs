using System;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading;
using System.IO;
using System.Collections;
using System.Text;


namespace ChatSever.Logic
{

    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    // Klassen der håntrer alt sever logik
    public class Server
    {
        #region Fields and Properties

        private IPAddress severIP = IPAddress.Parse("127.0.0.1");
        private int port = 8000;
        private TcpListener sever;
        private bool severRunning = false;

        private TcpClient client;
        private Thread listener;

        // Dette her er til at holde forbundet bruger og forbindlser. 
        public static Hashtable users = new Hashtable(5);
        public static Hashtable connections = new Hashtable(5);

        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;

        private static StreamWriter swSenderSender;
        private static NetworkStream netStream;
        private static SslStream ssl;

        #endregion

        #region Add and Remove users

        // Tilføjer brugeren og dens frobindlse til de to hastables. 
        // De bliver conetet til servern. 
        public static void AddUser(TcpClient tcpUser, string Username)
        {
            users.Add(Username, tcpUser);
            connections.Add(tcpUser, Username);

            Console.WriteLine("[{0}] " + connections[tcpUser] + " has connected ", DateTime.Now);
            SendAdminMessage(connections[tcpUser] + " has joined us");
        }

        // Fjerner brugeren fra de to hastables
        // Dicsonter brugern fra severn.
        public static void RemoveUser(TcpClient tcpUser)
        {
            if (connections[tcpUser] != null)
            {
                Console.WriteLine("[{0}] " + connections[tcpUser] + " has disconnected", DateTime.Now);
                SendAdminMessage(connections[tcpUser] + " has left us");

                users.Remove(connections[tcpUser]);
                connections.Remove(tcpUser);
            }
        }

        #endregion

        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(null, e);
        }

        #region Messenge Halding

        // Sender en adminstriv besked, hvem der er "logger på" og "logger af". 
        public static void SendAdminMessage(string Message)
        {
            e = new StatusChangedEventArgs("Administrator: " + Message);
            OnStatusChanged(e);

            TcpClient[] tcpClients = new TcpClient[users.Count];
            users.Values.CopyTo(tcpClients, 0);

            for (int i = 0; i < tcpClients.Length; i++)
            {
                try
                {
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }

                    netStream = tcpClients[i].GetStream();
                    ssl = new SslStream(netStream, true);
                    swSenderSender = new StreamWriter(ssl, Encoding.UTF8);
                    swSenderSender.WriteLine("Administrator: " + Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        // Sender en besked fra en bruger til alle andre.
        public static void SendMessage(string From, string Message)
        {
            e = new StatusChangedEventArgs(From + " says: " + Message);
            OnStatusChanged(e);

            TcpClient[] tcpClients = new TcpClient[users.Count];
            users.Values.CopyTo(tcpClients, 0);

            for (int i = 0; i < tcpClients.Length; i++)
            {
                try
                {
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }

                    netStream = tcpClients[i].GetStream();
                    ssl = new SslStream(netStream, false);
                    swSenderSender = new StreamWriter(ssl, Encoding.UTF8);
                    swSenderSender.WriteLine(From + " says: " + Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch 
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        #endregion

        // Stater på at lytte efter forbenlser.
        public void StartListening()
        {
            sever = new TcpListener(severIP, port);
            sever.Start();
            severRunning = true;

            listener = new Thread(KeepListening);
            listener.Start();
        }

        // En anden trød der bliver med med at lytte ved siden hovde trød
        private void KeepListening()
        {
            while (severRunning == true)
            {
                client = sever.AcceptTcpClient();
                Connection newConnection = new Connection(client);
            }
        }
    }
}

