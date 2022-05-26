using System.Net.Sockets;
using System.Text.Json;

class ClientObject
{
    TcpClient cli;

    ConnectionRequest request;

    StreamReader reader;
    StreamWriter writer;
    BloggingContext dataBase;


    public ClientObject(TcpClient client, BloggingContext db)
    {
        cli = client;
        var stream = cli.GetStream();
        reader = new StreamReader(stream);
        writer = new StreamWriter(stream);
        dataBase = db;
    }

    public async void BeginRead()
    {
        Console.WriteLine($"Connected to new client, IP: {cli.Client.RemoteEndPoint}");

        string receivedData = await reader.ReadLineAsync();

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
            var guid = Guid.NewGuid().ToString();
            dataBase.Printers.Add(new Printer() { printerSecret = guid, joinedOrg = req.orgName });
            WriteLine(guid);
        }
        else
        {
            if (dataBase.Users.Where(x => x.email == req.email).ToList().Count() > 0)
            {
                throw new Exception("User already exists");
            };
            dataBase.Users.Add(new User() { email = req.email, name = req.name, joinedOrg = req.orgName, passsword = req.password });
        }
    }

    void WriteLine(string line)
    {
        try
        {
            writer.WriteLine(line);
            writer.Flush();
        }
        catch
        {
            throw new Exception("Connection terminated by remote connection");
        }

    }
}

