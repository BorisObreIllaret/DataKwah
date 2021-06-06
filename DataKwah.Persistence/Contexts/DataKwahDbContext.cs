using System;
using DataKwah.Domain.Configurations;
using DataKwah.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataKwah.Persistence.Contexts
{
    public class DataKwahDbContext : DbContext
    {
        public DataKwahDbContext()
        {
        }

        public DataKwahDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddDomainConfigurations();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductState> ProductStates { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
