using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using ChatSever.Logic;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace ChatSever
{
    public class Program
    {
        public X509Certificate2 cert = new X509Certificate2("sever.pfx", "gruppe5");

        // Stater serveren.
        public static void Main(string[] args)
        {
            Program p = new Program(); // Laver serveren           
            Console.WriteLine();
            Console.WriteLine("Press enter to close program.");
            Console.WriteLine();
            Console.ReadLine();
        }

        public Program()
        {
            Console.Title = "Simpel Chat APP";
            Console.WriteLine("------ Chat Sever ------");
            Console.WriteLine("[{0}] Starting server...", DateTime.Now);

            //Laver en ny tcp listner til at lytte med.
            Server mainSever = new Server();

            mainSever.StartListening(); 
            Console.WriteLine("[{0}] Server is running properly and has statet listeners\n", DateTime.Now);
        }
    }
}
