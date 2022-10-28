using CoreBussiness.BussinessEntity.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreStrorage.EntityConfiguration;

public class CommentConfiguration:IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
    }
}