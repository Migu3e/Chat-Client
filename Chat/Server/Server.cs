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

        private IPHostEntry ipEntry { get; set; }
        private IPAddress ip { get; set; }
        private IPEndPoint ipEndPoint { get; set; }
        private Socket client { get; set; }

        public async Task ConnectToServer()
        {
            try
            {
                ipEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());

                // extracting local host ip (127.0.0.1)
                ip = ipEntry.AddressList[0];

                // connect the server socket to client socket
                ipEndPoint = new IPEndPoint(ip, 1234);

                client = new Socket(
                    ipEndPoint.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                await client.ConnectAsync(ipEndPoint);
                Console.WriteLine("Successfully connected to the server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to the server: {ex.Message}");
            }
        }
        public async Task SendMessage(string username)
        {
            while (true)
            {
                Console.WriteLine("Enter a message:");
                var message = Console.ReadLine();

                if (string.IsNullOrEmpty(message))
                    break;

                // Combine username and message separated by a delimiter
                var combinedMessage = $"{username}|{message}";

                // Convert combined message to bytes
                var messageBytes = Encoding.UTF8.GetBytes(combinedMessage);

                // Send the message
                await this.SendMessageAsync(messageBytes);
            }
        }

        public async Task ReceiveMessages()
        {
            while (true)
            {
                var buffer = new byte[1024];
                // Receive response
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
        
                if (received > 0)
                {
                    var messageString = Encoding.UTF8.GetString(buffer, 0, received);
            
                    int separatorIndex = -1;
                    for (int i = 0; i < messageString.Length; i++)
                    {
                        if (messageString[i] == ':')
                        {
                            separatorIndex = i;
                            break;
                        }
                    }
                    
                    var catchUsername = messageString.Substring(0, separatorIndex);
                    var catchMessage = messageString.Substring(separatorIndex + 1);

                    Console.WriteLine($"<{catchUsername}>{catchMessage}");

                   
                }
                else
                {
                    // Handle disconnection or empty message
                    Console.WriteLine("Disconnected or received empty message.");
                    break; // Exit the loop
                }
            }
        }

        public void StartReceivingMessagesInBackground()
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    await ReceiveMessages();
                }
            });
        }


        private async Task SendMessageAsync(byte[] messageBytes)
        {
            // Send the message
            await this.client.SendAsync(messageBytes, SocketFlags.None);
        }

        
    }
}
