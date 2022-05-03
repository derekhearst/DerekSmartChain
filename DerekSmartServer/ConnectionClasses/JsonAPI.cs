using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



internal class ConnectionRequest
{
    public bool firstTimeRequest { get; set; }
    public bool isUser { get; set; }
    public bool isPrinter { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string printerID { get; set; }
}

