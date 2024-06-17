using System.Net;
using System.Net.Sockets;
using System.Text;
using Chat.Interfaces;

namespace ClientServer.Server;

public class ServerRecieveMassege: IServerRecieveMassage
{
    private IServerCommands ServerCommands { set; get; }

    public ServerRecieveMassege(IServerCommands serverCommands)
    {
        ServerCommands = serverCommands;
    }
    public async Task ReceiveMessages()
    {
        
            var buffer = new byte[1024];

            // Receive response
            var received = await ServerCommands.client.ReceiveAsync(buffer, SocketFlags.None);

            var messageString = Encoding.UTF8.GetString(buffer, 0, received);
            var formattedMessage = await ChangeMessage(messageString);

            Console.WriteLine(formattedMessage);
        
    }
    public async Task<string> ChangeMessage(string messageString)
    {
        if (messageString.StartsWith("<"))
        {
            return messageString;
        }

        return messageString;
    }


    public async Task StartReceivingMessagesInBackground()
    {
        await Task.Run(async () =>
        {
            while (ServerCommands.IsConnected)
            {
                await ReceiveMessages();
            }
        });
    }

    public IPEndPoint ipEndPoint { get; set; }
    public Socket client { get; set; }
}