using System.Net.Sockets;
using System.Text;
using Chat.Const;
using Chat.Interfaces;

namespace ClientServer.Server;

public class ServerSendMasseges : IServerSendMassege
{
    private ServerCommands ServerCommands { set; get; }

    public ServerSendMasseges(ServerCommands serverCommands)
    {
        ServerCommands = serverCommands;
    }


    public async Task SendMessage(string username)
    {
        while (ServerCommands.IsConnected)
        {
            var message = Console.ReadLine();

            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine(ConstMasseges.EmptyString);
                continue;
            }

            if (message.StartsWith("/leave"))
            {
                Console.Clear();
            }

            if (message.StartsWith("/jroom"))
            {
                Console.Clear();
            }
            if (message.StartsWith("/private"))
            {
                Console.Clear();
            }

            // Combine username and message separated by a |
            var combinedMessage = $"{username}|{message}";

            // Convert combined message to bytes
            var messageBytes = Encoding.UTF8.GetBytes(combinedMessage);

            // Send the message
            await SendMessageAsync(messageBytes);
        }
    }
    public async Task SendMessageAsync(byte[] messageBytes)
    {
        // Send the message
        await ServerCommands.client.SendAsync(messageBytes, SocketFlags.None);
    }
    public void SendInstractions()
    {
        Console.WriteLine(ConstMasseges.HelpMessage);
    }


}