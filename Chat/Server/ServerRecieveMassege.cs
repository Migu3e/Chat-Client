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
        while (true)
        {
            var buffer = new byte[1024];

            // Receive response
            var received = await ServerCommands.client.ReceiveAsync(buffer, SocketFlags.None);

            if (received > 0)
            {
                var messageString = Encoding.UTF8.GetString(buffer, 0, received);

                int separatorIndex = messageString.IndexOf(':');
                if (separatorIndex != -1)
                {
                    var catchUsername = messageString.Substring(0, separatorIndex);
                    var catchMessage = messageString.Substring(separatorIndex + 1);

                    Console.WriteLine($"<{catchUsername}>{catchMessage}");
                }
                else
                {
                    Console.WriteLine("Received message with invalid format.");
                }
            }
            else
            {
                ServerCommands.Disconnect();
                break;
            }
        }
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