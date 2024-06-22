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
            string username = this.Username;

            Server.Server server = new Server.Server(); // Use fully qualified name
            Console.Write(ConstMasseges.EnterFiveDigits);
            string ipstring = Console.ReadLine();
            Console.WriteLine(ConstMasseges.AfterIp);
            IPAddress ip = IPAddress.Parse(ipstring);
            await server.ServerCommands.ConnectToServer(username,ip); // Await the connection task

            
            _ = server.RecieveMassage.StartReceivingMessagesInBackground();
            await server.MassegeSend.SendMessage(username);
        }
    }
}