using Biker.Models;
using Microsoft.EntityFrameworkCore;

namespace Biker.Persistence
{
    public class BikerDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Model> Models { get; set; }

        public BikerDbContext(DbContextOptions<BikerDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BikeFeature>().HasKey(bf =>
              new
              {
                  bf.BikeId,
                  bf.FeatureId
              });

            modelBuilder.Entity<Bike>().OwnsOne(
                b => b.Contact,
                c =>
                {
                    c.Property(p => p.Name).HasColumnName("ContactName");
                    c.Property(p => p.Email).HasColumnName("ContactEmail");
                    c.Property(p => p.Phone).HasColumnName("ContactPhone");
                });
        }
    }
}
