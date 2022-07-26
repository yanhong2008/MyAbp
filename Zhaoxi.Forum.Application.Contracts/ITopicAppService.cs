using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zhaoxi.Forum.Application.Contracts
{
    public interface ITopicAppService : IApplicationService
    {
        Task<bool> AnyAsync();
        Task<TopicDto> GetTopicAsync(long id);

        Task<PagedResultDto<TopicDto>> GetTopicListAsync(long categoryId, int page = 10, int limit = 5);

        /// <summary>
        /// xlsx数据导入
        /// </summary>
        Task ImportAsync(IEnumerable<TopicImportDto> importDtos);
    }
}
