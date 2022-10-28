using CoreBussiness.BussinessEntity.AppServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreStrorage.EntityConfiguration;

public class AppServiceConfiguration:IEntityTypeConfiguration<AppService>
{
    public void Configure(EntityTypeBuilder<AppService> builder)
    {
        builder.Property(x => x.PhoneNumber).HasMaxLength(11);
        builder.HasOne(x => x.User).WithMany(x => x.AppServices).HasForeignKey(x => x.UserId);
        
    }
}