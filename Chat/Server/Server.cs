namespace Chat.Server;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    private IPHostEntry ipEntry;
    private IPAddress ip;
    

    // connect the server socket to client socket
    private IPEndPoint ipEndPoint;

    private Socket client;
    public async Task ConnectToServer()
    {
        
        this.ipEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());

        // extracting local host ip (127.0.1.1)
        this.ip = ipEntry.AddressList[0];

        // connect the server socket to client socket
        this.ipEndPoint = new(ip,1234);

        this.client = new
        (
            ipEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );
    
        await client.ConnectAsync(ipEndPoint);
            
    }


    public async Task<bool> SendMessege()
    {
        


        while (true)
        {
            Console.WriteLine("send a messege:");
            var message = Console.ReadLine();
            if (message == "---")
            {
                return false;
            }
            //covert byte to messeges
            var messageBytes = Encoding.UTF8.GetBytes(message);

            await this.client.SendAsync(messageBytes, SocketFlags.None);

            var buffer = new byte[1_024];
            //we recevived the messege in byte
            var received = await client.ReceiveAsync(buffer, SocketFlags.None);


            var messageString = Encoding.UTF8.GetString(buffer,0, received);

            Console.WriteLine("recived message = " + messageString);
        }
    }
}