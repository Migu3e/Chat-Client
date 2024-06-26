using System.Net;
using System.Net.Sockets;
using Chat.Interfaces;

namespace ClientServer.Server
{
    public class Server
    {
        
        public Server()
        {
            ServerCommands = new ServerCommands();
            RecieveMassage = new ServerRecieveMassege(ServerCommands);
            MassegeSend = new ServerSendMasseges(ServerCommands);
            IsConnected = true;
        }



        public IPEndPoint ipEndPoint { get; set; }
        public Socket client { get; set; }
        public bool IsConnected { get; set; }

        public IServerCommands ServerCommands { get; set; }
        public IServerRecieveMassage RecieveMassage{ get; set; }
        public IServerSendMassege MassegeSend { get; set; }
        

    }
}