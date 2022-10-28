using CoreBussiness.BussinessEntity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreStrorage.EntityConfiguration;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.PhoneNumber).HasMaxLength(11);
    }
}