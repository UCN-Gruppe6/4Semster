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
        private string strResponse;

        private BinaryWriter biWriter;
        private BinaryReader biReader;
        
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

            biReader = new BinaryReader(tcpClient.GetStream());
            biWriter = new BinaryWriter(tcpClient.GetStream());

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
                while((strResponse = srReceiver.ReadLine()) != "")
                {
                    if (strResponse == null)
                    {
                        Server.RemoveUser(tcpClient);
                    }
                    else
                    {
                        Server.SendMessage(currUser, strResponse);
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
