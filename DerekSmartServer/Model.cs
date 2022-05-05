using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


public class BloggingContext : DbContext
{
    public DbSet<Printer> Printers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Org> Orgs { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        DbPath = @"C:\Users\derek\source\repos\DerekSmartChain\DerekSmartServer\dereksmart.db";
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Printer
{
    public int ID { get; set; }
    public string joinedOrg { get; set; }
    public string printerName { get; set; }
    public string printerSecret { get; set; }
}

public class User
{
    public int ID { get; set; }
    public string joinedOrg { get; set; }
    public string email { get; set; }
    public string passsword { get; set; }
    public string name { get; set; }
}

public class Org
{
    public int ID { get; set; }
    public string orgName { get; set; }

}