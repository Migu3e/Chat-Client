using System.Net;
using System.Net.Sockets;
using Chat.Interfaces;

namespace ClientServer.Server
{
    public class Server : IServer
    {


        public Server() {}
        public IPEndPoint ipEndPoint { get; set; }
        public Socket client { get; set; }
    }
}