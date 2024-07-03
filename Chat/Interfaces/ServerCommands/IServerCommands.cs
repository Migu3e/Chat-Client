using System.Net;
using System.Net.Sockets;
using ClientServer.Server;

namespace Chat.Interfaces;

public interface IServerCommands
{

    Task ConnectToServer(string username, IPAddress sip);
    void Disconnect();


}