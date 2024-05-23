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
            Console.WriteLine("1 - Connect\n2 - Message");
            int option = int.Parse(Console.ReadLine());
            string username = this.username;

            switch (option)
            {
                case 1:
                    // Connect logic here
                    break;

                case 2:
                    Server.Server server = new Server.Server(); // Use fully qualified name
                    await server.ConnectToServer(); // Await the connection task
    
                    // Start receiving messages in the background
                    server.StartReceivingMessagesInBackground();
    
                    var sendMessageTask = server.SendMessage(username);
                    // No need to explicitly start receiving messages here

                    // Wait for sending messages task to complete
                    await sendMessageTask;
                    break;

            }
        }

    }
}