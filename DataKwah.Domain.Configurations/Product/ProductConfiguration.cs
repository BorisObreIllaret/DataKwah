using DataKwah.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataKwah.Domain.Configurations.Product
{
    public class ProductConfiguration : IEntityTypeConfiguration<Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Entities.Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.Property(product => product.Id).ValueGeneratedOnAdd();
            builder.HasIndex(product => product.Asin).IsUnique();
            builder.HasOne(product => product.ProductState)
                .WithOne(productState => productState.Product)
                .HasForeignKey<ProductState>(productState => productState.Id);
        }
    }
}
