using Zhaoxi.Forum.Domain.Shared;

namespace Zhaoxi.Forum.Application.Contracts.Auth;

public interface IAuthAppService
{
    /// <summary>
    /// 注册
    /// </summary>
    Task<ApiResponse<LoginDto>> RegisterAsync(RegistInput input);

    /// <summary>
    /// 登录
    /// </summary>
    Task<ApiResponse<LoginDto>> LoginAsync(LoginInput input);
}
