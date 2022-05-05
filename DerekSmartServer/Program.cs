using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.Json;
using Microsoft.Data.Sqlite;
 

TcpListener listener = new(433);
listener.Start();
List<ClientObject> handlers = new List<ClientObject>();

var dataBase = new BloggingContext();



while (true)
{
    var cli = await listener.AcceptTcpClientAsync();
    var stream = cli.GetStream();
    await Task.Run(() => HandleConnection(cli,dataBase));

}


void HandleConnection(TcpClient client,BloggingContext db)
{
    ClientObject cli = new(client,db);
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