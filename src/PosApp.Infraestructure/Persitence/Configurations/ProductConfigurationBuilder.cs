using Doamin.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PosApp.Infrastructure.Persitence.Configurations;

public class ProductConfigurationBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(prop => prop.Name)
            .HasColumnName("ProductName")
            .IsRequired();  

        //configure product entiti type
    }
}
