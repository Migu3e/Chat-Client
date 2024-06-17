using ClientServer.Server;

namespace Chat.Interfaces;

public interface IServerRecieveMassage
{
    Task ReceiveMessages();
    Task StartReceivingMessagesInBackground();


}