using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Data.OleDb;
 

TcpListener listener = new(433);
listener.Start();
List<ClientObject> handlers = new List<ClientObject>();




while (true)
{
    var cli = await listener.AcceptTcpClientAsync();
    var stream = cli.GetStream();
    await Task.Run(() => HandleConnection(cli));

}


async void HandleConnection(TcpClient client)
{
    ClientObject cli = new(client);
    handlers.Add(cli);
    try
    {
        cli.BeginRead();
    }
    catch
    {
        handlers.Remove(cli);
    }
    
}