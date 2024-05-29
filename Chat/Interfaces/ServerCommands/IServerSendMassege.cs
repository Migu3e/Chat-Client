using System.Net;

namespace Chat.Interfaces;

public interface IServerSendMassege
{
    Task SendMessage(string username);
    public void SendInstractions();
    Task SendMessageAsync(byte[] messageBytes);
}