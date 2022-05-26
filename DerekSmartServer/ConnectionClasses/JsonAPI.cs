internal class ConnectionRequest
{
    public bool firstTimeRequest { get; set; }
    public bool isUser { get; set; }
    public bool isPrinter { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string printerSecret { get; set; }
    public string orgName { get; set; }
    public string name { get; set; }
}

