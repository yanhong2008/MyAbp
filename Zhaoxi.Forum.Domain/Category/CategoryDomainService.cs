using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Zhaoxi.Forum.Domain.Topic;

namespace Zhaoxi.Forum.Domain.Category
{
    public class CategoryDomainService : DomainService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly ITopicRepository _topicRepository;

        public CategoryDomainService(ICategoryRepository categoryRepository, ITopicRepository topicRepository)
        {
            _categoryRepository = categoryRepository;
            _topicRepository = topicRepository;
        }

        //public async Task<Tuple<CategoryEntity , int>> GetAsync(long id)
        public async Task<(CategoryEntity category, int topicTimes)> GetAsync(long id)
        {
            var categoryQueryable = await _categoryRepository.GetQueryableAsync();
            var topicQueryable = await _topicRepository.GetQueryableAsync();
            var category = await _categoryRepository.GetAsync(id);

            // 自身和子板块主题数
            var categoryIdArray = GetSubCategoryIds(categoryQueryable, id);
            var topicTimes = topicQueryable.Count(t => categoryIdArray.Contains(t.Category.Id));
            //var result=new Tuple<CategoryEntity, int>(category, topicTimes);

            return (category, topicTimes);
        }

        public async Task<int> GetTopicTimes(ConcurrentBag<long> categoryIdArray)
        {
            var topicQueryable= await _topicRepository.GetQueryableAsync();
           return topicQueryable.Count(t => categoryIdArray.Contains(t.Category.Id));
        }


        public ConcurrentBag<long> GetSubCategoryIds(IQueryable<CategoryEntity> categoryQueryable, long parentCategoryId)
        {
            var categoryIdList = new ConcurrentBag<long>();
            var stackIds = new ConcurrentStack<long>();
            stackIds.Push(parentCategoryId);
            while (stackIds.TryPop(out long categoryId))
            {
                categoryIdList.Add(categoryId);
                var subCategoryIds = categoryQueryable
                    .Where(c => c.ParentCategory == categoryId)
                    .Select(c => c.Id).ToArray();
                if (subCategoryIds.Any())
                {
                    foreach (var item in subCategoryIds)
                    {
                        stackIds.Push(item);
                    }
                }
            }

            return categoryIdList;
        }
    }
}
