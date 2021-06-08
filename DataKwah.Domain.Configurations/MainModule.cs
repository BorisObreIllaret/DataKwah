using DataKwah.Domain.Configurations.Product;
using Microsoft.EntityFrameworkCore;

namespace DataKwah.Domain.Configurations
{
    public static class MainModule
    {
        public static void AddDomainConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductStateConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }
    }
}