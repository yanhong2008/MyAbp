using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zhaoxi.Forum.Domain.Category;

namespace Zhaoxi.Forum.EntityFrameworkCore
{
    public class CategoryRepository : EfCoreRepository<ForumDbContext, CategoryEntity, long>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<ForumDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds)
        {
            return (await GetQueryableAsync()).Where(t=>userIds.Contains(t.Id)).ToList();
        }
    }
}
