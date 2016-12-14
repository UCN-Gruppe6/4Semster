using System;
using ChatSever.Logic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Threading;
using System.Net;


namespace ChatSever
{
    public class Program
    {
        // Stater serveren.
        public static void Main(string[] args)
        {
            Program p = new Program(); // Laver serveren           
            Console.WriteLine();
            Console.WriteLine("Press enter to close program.");
            Console.WriteLine();
            Console.ReadLine();
        }

        public X509Certificate2 cert = new X509Certificate2(@"C:\Users\tinsf\Source\Repos\4Semster\ChatAPP\ChatSever\sever.pfx", "gruppe5");

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
