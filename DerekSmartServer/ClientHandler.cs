using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

class ClientHandler
{
    TcpClient cli = null;

    public ClientHandler(TcpClient client)
    {            
        cli = client;
        BeginRead();
    }

    async void BeginRead()
    {
        var stream = cli.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);        
        writer.WriteLine("Connected!");
        writer.Flush();
        Console.WriteLine("Connected!");
        while (true)
        {
            Console.WriteLine(await reader.ReadLineAsync());
            await writer.WriteLineAsync("Received Info!");
            writer.Flush();
        }
    }
}

