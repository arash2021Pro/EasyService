using CoreBussiness.BussinessEntity.AppServices;
using CoreBussiness.BussinessEntity.Comments;
using CoreBussiness.BussinessEntity.Users;

namespace CoreBussiness.ManagerService;

public interface IManagerService
{
    public IUserService UserService { get; set; }
    public IAppService AppService { get; set; }
    public ICommentService CommentService { get; set; }
}