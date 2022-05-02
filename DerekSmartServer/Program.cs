using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.Json;
 

TcpListener listener = new(433);
listener.Start();


while (true)
{
    var cli = await listener.AcceptTcpClientAsync();
    var stream = cli.GetStream();
    await Task.Run(() => HandleConnection(cli));

}


async void HandleConnection(TcpClient client)
{

    ClientHandler cli = new(client);

}