using System.Data.Entity;
using WebAPI.Database.Models;

namespace WebAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("WebAPIConnectionString")
        {

        }

        public DbSet<User> users { get; set; }
    }
}