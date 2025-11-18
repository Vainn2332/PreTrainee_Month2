using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.InfrastructureLayer.DataBaseContext
{
    public class ProductDBConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.ID);
            builder.HasIndex(product => product.Name).IsUnique();
        }
    }
}
