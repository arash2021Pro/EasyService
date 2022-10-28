using CoreBussiness.BussinessEntity.AppServices;
using CoreBussiness.BussinessEntity.Comments;
using CoreBussiness.BussinessEntity.Users;

namespace CoreBussiness.ManagerService;

public class ManagerService:IManagerService
{
    public ManagerService(IUserService userService, IAppService appService, ICommentService commentService)
    {
        UserService = userService;
        AppService = appService;
        CommentService = commentService;
    }

    public IUserService UserService { get; set; }
    public IAppService AppService { get; set; }
    public ICommentService CommentService { get; set; }
}