using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zhaoxi.Forum.Application.Contracts.Category
{
    public interface ICategoryAppService : IApplicationService
    {

        Task<CategoryDto> GetAsync(long id);

        Task<ListResultDto<CategoryDto>> GetListAsync();

        /// <summary>
        /// xlsx数据导入
        /// </summary>
        Task ImportAsync(IEnumerable<CategoryImportDto> importDtos);
        Task<bool> AnyAsync();
    }
}
