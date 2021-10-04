using Microsoft.EntityFrameworkCore;
using TestLib.Models;

namespace TestLib.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientLimits> ClientsLimits { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
