using System.Net;
using System.Net.Sockets;

namespace Chat.Interfaces;

public interface IServerCommands
{
    IPEndPoint IpEndPoint { get; set; }
    Socket client { get; set; }
    public bool IsConnected { get; set; }
    IServerRecieveMassage _recieveMassage { get; set; }
    IServerSendMassege _sendMassege  { get; set; }
    Task ConnectToServer(string username, IPAddress sip);
    void Disconnect();


}