using DataKwah.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataKwah.Domain.Configurations.Product
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(review => review.Id);
            builder.Property(review => review.Id).ValueGeneratedOnAdd();
            builder.HasIndex(review => review.Asin).IsUnique();
            builder.HasOne(review => review.Product)
                .WithMany(product => product.Reviews)
                .HasForeignKey(review => review.ProductId);
        }
    }
}
