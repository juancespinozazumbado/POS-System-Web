using Doamin.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PosApp.Infrastructure.Persitence.Configurations;

public class ProductConfigurationBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        //configure product entiti type
    }
}
