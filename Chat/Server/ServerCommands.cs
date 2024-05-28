using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Chat.Const;
using Chat.Interfaces;

namespace ClientServer.Server
{
    public class ServerCommands : IServerCommands
    {
        public IPEndPoint IpEndPoint { get; set; }
        public Socket client { get; set; }

        public bool IsConnected { get; set; }

        public async Task ConnectToServer(string username, IPAddress sip)
        {
            try
            {
                // connect the server socket to client socket
                IpEndPoint = new IPEndPoint(sip, ConstMasseges.ipPort);

                client = new Socket(
                    IpEndPoint.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                await client.ConnectAsync(IpEndPoint);
                Console.WriteLine("Successfully connected to the server.");
                IsConnected = true;
                byte[] usernameBytes = Encoding.UTF8.GetBytes(username);
                await client.SendAsync(usernameBytes, SocketFlags.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to the server: {ex.Message}");
            }
        }

        public async Task SendMessage(string username)
        {
            while (IsConnected)
            {
                var message = Console.ReadLine();

                if (string.IsNullOrEmpty(message))
                {
                    Console.WriteLine("Cannot send an empty string");
                    continue;
                }

                // Combine username and message separated by a |
                var combinedMessage = $"{username}|{message}";

                // Convert combined message to bytes
                var messageBytes = Encoding.UTF8.GetBytes(combinedMessage);

                // Send the message
                await SendMessageAsync(messageBytes);
            }
        }
        public void Disconnect()
        {
            IsConnected = false;
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        public async Task ReceiveMessages()
        {
            while (IsConnected)
            {
                var buffer = new byte[1024];
                // Receive response
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);

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
                    Disconnect();
                    break;
                }
            }
        }

        public async Task StartReceivingMessagesInBackground()
        {
            await Task.Run(async () =>
            {
                while (IsConnected)
                {
                    await ReceiveMessages();
                }
            });
        }

        public async Task SendMessageAsync(byte[] messageBytes)
        {
            // Send the message
            await client.SendAsync(messageBytes, SocketFlags.None);
        }

        public void SendInstractions()
        {
            Console.WriteLine( "The commands are:\n" +
                               "/list - Display the connected users\n" +
                               "/logout - Disconnect from the server\n" +
                               "/croom <roomname> - Create a new room with the specified name\n" +
                               "/jroom <roomname> - Join an existing room with the specified name\n" +
                               "/iroom <username> <roomname> - Invite a user to a specified room\n" +
                               "/leave - Leave the current room and return to the main room\n" +
                               "/list rooms - List all rooms and their members\n" +
                               "/whisper <username> <message> - Send a private message to a specific user\n" +
                               "/help - Display this list of commands");

        }
    }
}
