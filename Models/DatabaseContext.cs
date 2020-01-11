using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace InventoryAllTheThings.Models
{
  public partial class DatabaseContext : DbContext
  {
    public DbSet<Inventory> InventoryItems { get; set; }


    public string ConnectionString { get; set; }

    public DatabaseContext(IConfiguration configuration)
    {
      this.ConnectionString = configuration["ConnectionString"];
    }

    private string ConvertPostConnectionToConnectionString(string connection)
    {
      var _connection = connection.Replace("postgres://", String.Empty);
      var output = Regex.Split(_connection, ":|@|/");
      return $"server={output[2]};database={output[4]};User Id={output[0]}; password={output[1]}; port={output[3]}";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var envConn = Environment.GetEnvironmentVariable("DATABASE_URL");
        //#warning Update this connection string to point to your own database.
        var conn = this.ConnectionString; // configuration["ConnectionString"];
        // "server=localhost;database=inventoryofallthings;user id=postgres;password=Schn743!";
        if (envConn != null)
        {
          conn = ConvertPostConnectionToConnectionString(envConn);
        }
        optionsBuilder.UseNpgsql(conn);
      }
    }



  }
}
