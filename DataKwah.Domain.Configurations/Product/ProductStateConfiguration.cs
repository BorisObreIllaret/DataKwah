using DataKwah.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataKwah.Domain.Configurations.Product
{
    public class ProductStateConfiguration : IEntityTypeConfiguration<ProductState>
    {
        public void Configure(EntityTypeBuilder<ProductState> builder)
        {
            builder.HasKey(product => product.Id);
        }
    }
}