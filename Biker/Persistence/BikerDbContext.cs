using Biker.Models;
using Microsoft.EntityFrameworkCore;

namespace Biker.Persistence
{
    public class BikerDbContext : DbContext
    {
        public BikerDbContext(DbContextOptions<BikerDbContext> options)
             : base(options)
        {

        }
        public DbSet<Make> Makes { get; set; }
    }
}
