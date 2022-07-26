using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Application.Contracts.User;
using Zhaoxi.Forum.Domain;
using Zhaoxi.Forum.Domain.User;

namespace Zhaoxi.Forum.Application;

public class UserAppService : ApplicationService, IUserAppService
{
    private readonly IUserRepository _userRepository;

    public UserAppService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<bool> AnyAsync()
    {
        return await _userRepository.GetCountAsync() > 0;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Permission")]
    public async Task<bool> Any2Async()
    {
        return await _userRepository.GetCountAsync() > 0;
    }

    public async Task ImportAsync(IEnumerable<UserImportDto> importDtos)
    {
        var users = ObjectMapper.Map<IEnumerable<UserImportDto>,
            IEnumerable<UserEntity>>(importDtos);

        await _userRepository.InsertManyAsync(users);
    }
}
