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
        
        
        ServerSendMasseges SendMasseges { get; set; }
        ServerRecieveMassege RecieveMassege  { get; set; }
        
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
                Console.WriteLine(ConstMasseges.ConnectedToServerSuccess);
                IsConnected = true;
                byte[] usernameBytes = Encoding.UTF8.GetBytes(username);
                await client.SendAsync(usernameBytes, SocketFlags.None);
                Console.WriteLine(ConstMasseges.HelpMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to the server: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            
            IsConnected = false;
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }



    }
}
