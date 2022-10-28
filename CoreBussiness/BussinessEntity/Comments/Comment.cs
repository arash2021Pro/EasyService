using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.CoreEntity;

namespace CoreBussiness.BussinessEntity.Comments;

public class Comment:Core
{
    public string?CommentMessage { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}