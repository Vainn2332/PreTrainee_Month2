using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreTrainee_Month2.CoreLayer;

namespace PreTrainee_Month2.InfrastructureLayer.DataBaseContext
{
    public class UserDBConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(user => user.Products)
                .WithOne(product => product.Owner)//связь один ко многим по FK UserID
                .HasForeignKey(product => product.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(user => user.ID);
        }
    }
}
