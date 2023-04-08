using SignalR.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SignalR.API.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Visitor> Visitors { get; set; }
    }
}