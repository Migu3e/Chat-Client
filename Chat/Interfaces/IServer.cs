using System.Net;
using System.Net.Sockets;

namespace Chat.Interfaces;

public interface IServer
{
    IPEndPoint ipEndPoint { get; set; }
    Socket client { get; set; }
    public bool IsConnected { get; set; }

}