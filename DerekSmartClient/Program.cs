using System.Net;
using System.Net.Sockets;

TcpClient client = new();
IPAddress address = IPAddress.Parse("127.0.0.1");
client.Connect(address, 433);
var stream = client.GetStream();
StreamWriter sw = new StreamWriter(stream);
StreamReader sr = new StreamReader(stream);

while (true)
{
    sw.WriteLine(Console.ReadLine());
    sw.Flush();
    Console.WriteLine(await sr.ReadLineAsync());
}






