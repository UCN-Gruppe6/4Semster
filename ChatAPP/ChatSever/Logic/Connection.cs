using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;
using ChatSever.Logic;

namespace ChatSever.Logic
{
    // Denne klasse håntere alle forbinlser. Der er en instens af Connection for vær bruger der er forbundet.  
    public class Connection
    {
        TcpClient tcpClient;
        private Thread thrSender; // Den trød der sender data til clienten.
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currUser;
        private string toUser;
        private string strResponse;
        
        // Stater en forbinlse og begynder at arkseteper bruger. 
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
        }

        private void AcceptClient()
        {
            srReceiver = new StreamReader(tcpClient.GetStream());
            swSender = new StreamWriter(tcpClient.GetStream());

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
            try
            {
                toUser = srReceiver.ReadLine();

                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    if (strResponse == null)
                    {
                        Server.RemoveUser(tcpClient);
                    }
                    else if(Server.users.Contains(toUser))
                    {
                        Server.SendPrivateMessage(currUser, strResponse, toUser);
                        Console.WriteLine("[{0}] " + currUser + " has sent a private message to " + toUser, DateTime.Now);
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
