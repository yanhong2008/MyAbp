using Volo.Abp.Application.Services;
using Zhaoxi.Forum.Domain.Shared;

namespace Zhaoxi.Forum.Application.Contracts.User;

public interface IUserAppService : IApplicationService
{
    Task<bool> AnyAsync();

    /// <summary>
    /// xlsx数据导入
    /// </summary>
    Task ImportAsync(IEnumerable<UserImportDto> importDtos);
}
