using Crud_Operation.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud_Operation.Models.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreateDate).IsRequired(false);
            builder.Property(x => x.UpdateDate).IsRequired(false);
        }
    }
}
