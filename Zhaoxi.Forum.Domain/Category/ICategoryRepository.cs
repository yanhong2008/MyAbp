using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Zhaoxi.Forum.Domain.Category
{
    public interface ICategoryRepository:IRepository<CategoryEntity,long>
    {
        Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds);
    }
}
