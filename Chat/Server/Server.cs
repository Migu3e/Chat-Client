using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer.Server
{
    public class Server
    {
        public Server() {}
        private IPEndPoint ipEndPoint { get; set; }
        private Socket client { get; set; }
        private static bool isConnected{ get; set; }
        

        public async Task ConnectToServer(string username,IPAddress sip)
        {
            try
            {
                // connect the server socket to client socket
                ipEndPoint = new IPEndPoint(sip, 1234);

                client = new Socket(
                    ipEndPoint.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                
                
                await client.ConnectAsync(ipEndPoint);
                Console.WriteLine("Successfully connected to the server.");
                isConnected = true;
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
            while (isConnected)
            {
                var message = Console.ReadLine();

                if (string.IsNullOrEmpty(message))
                {
                    Console.WriteLine("cannot send empty string");
                }

                // Combine username and message separated by a |
                var combinedMessage = $"{username}|{message}";

                // Convert combined message to bytes
                var messageBytes = Encoding.UTF8.GetBytes(combinedMessage);

                // Send the message
                await this.SendMessageAsync(messageBytes);
            }
        }
        private void Disconnect()
        {
            isConnected = false;
        }
        private async Task ReceiveMessages()
        {
            while (isConnected)
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

                while (isConnected)
                {
                    await ReceiveMessages();
                }

        }


        private async Task SendMessageAsync(byte[] messageBytes)
        {
            // Send the message
            await this.client.SendAsync(messageBytes, SocketFlags.None);
        }

        
    }
}
