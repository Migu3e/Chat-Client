using System.Net;
using System.Net.Sockets;

namespace Chat.Interfaces;

public interface IServerCommands
{
    IPEndPoint IpEndPoint { get; set; }
    Socket client { get; set; }
    public bool IsConnected { get; set; }

    Task ConnectToServer(string username, IPAddress sip);
    Task SendMessage(string username);
    void Disconnect();
    Task ReceiveMessages();
    Task StartReceivingMessagesInBackground();
    void SendInstractions();

    Task SendMessageAsync(byte[] messageBytes);

}