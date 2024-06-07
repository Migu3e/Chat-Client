using ClientServer.MainOperations;
using Client.MongoDB;
using ClientServer.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Chat.Const;

namespace ClientServer.ProgramOptions
{
    public class ProgramClientOptions : IProgramClientOptions
    {
        public string Username { get; private set; }

        public ProgramClientOptions(string username)
        {
            this.Username = username;
        }

        public async Task ProgramClient()
        {
            Console.WriteLine(ConstMasseges.MenuProgramClient);
            int option = int.Parse(Console.ReadLine());
            string username = this.Username;

            switch (option)
            {
                case 1:
                    Server.Server server = new Server.Server(); // Use fully qualified name
                    Console.WriteLine(ConstMasseges.EnterFiveDigits);
                    string ipstring = "10.0." + Console.ReadLine();
                    IPAddress ip = IPAddress.Parse(ipstring);
                    await server.ServerCommands.ConnectToServer(username,ip); // Await the connection task

                    
                    _ = server.RecieveMassage.StartReceivingMessagesInBackground();

                    
                    await server.MassegeSend.SendMessage(username);

                    // No need to explicitly start receiving messages here



                    // Start receiving messages in the background

                    break;
            }
        }
    }
}