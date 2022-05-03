using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text.Json;

class ClientObject
{
    TcpClient cli;

    ConnectionRequest request;

    StreamReader reader;
    StreamWriter writer;



    public ClientObject(TcpClient client)
    {            
        cli = client;
        var stream = cli.GetStream();
        reader = new StreamReader(stream);
        writer = new StreamWriter(stream);
    }

    public async void BeginRead()
    {
        writer.WriteLine("You have connected to DerekSmart");
        writer.Flush();
        Console.WriteLine($"Connected to new client, IP: {cli.Client.RemoteEndPoint}");
        string receivedData;
        try
        {
            receivedData = await reader.ReadLineAsync();
        }
        catch
        {
            throw new Exception("Connection terminated by remote connection");
        }         
        if (receivedData == null) { throw new Exception("No Data Received."); }        
        try
        {
            request = JsonSerializer.Deserialize<ConnectionRequest>(receivedData);
        }
        catch
        {
            throw new Exception("Received data was not following spec");
        }
        if (request.firstTimeRequest) { await initFirstTime(request); }
        if (request.isUser) { await initUser(request); }
        if (request.isPrinter) { await initPrinter(request); }
    }

    async Task initPrinter(ConnectionRequest req)
    {
        

    }

    async Task initUser(ConnectionRequest req)
    {
       
    }

    async Task initFirstTime(ConnectionRequest req)
    {
        if (req.isPrinter)
        {
            

        }
        else
        {

        }
    }
}

