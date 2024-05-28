using ClientServer.MainOperations;
using Client.Firestore;
using ClientServer.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer.ProgramOptions
{
    public class ProgramClientOptions
    {
        private string username;

        public ProgramClientOptions(string username)
        {
            this.username = username;
        }

        public async Task ProgramClient()
        {
            Console.WriteLine("--------------------------------------------------\n1 - Connect\n2 - Message\n--------------------------------------------------\n");
            int option = int.Parse(Console.ReadLine());
            string username = this.username;

            switch (option)
            {
                case 1:
                    // Connect logic here
                    break;

                case 2:
                    Server.ServerCommands server = new Server.ServerCommands(); // Use fully qualified name
                    Console.WriteLine("enter last 5 digits");
                    string ipstring ="192.168."+ Console.ReadLine();
                    IPAddress ip = IPAddress.Parse(ipstring);
                    await server.ConnectToServer(this.username,ip); // Await the connection task

                    server.SendInstractions();
                    var sendMessageTask = server.SendMessage(username);
                    
                    // No need to explicitly start receiving messages here

                    // Wait for sending messages task to complete
                    await sendMessageTask;
                    
                    // Start receiving messages in the background
                    server.StartReceivingMessagesInBackground();
                    

                    break;

            }
        }

    }
}