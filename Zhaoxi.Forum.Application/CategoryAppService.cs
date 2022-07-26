using System.Collections.Concurrent;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zhaoxi.Forum.Application.Contracts.Category;
using Zhaoxi.Forum.Domain;
using Zhaoxi.Forum.Domain.Category;
using Zhaoxi.Forum.Domain.Topic;

namespace Zhaoxi.Forum.Application;

public class CategoryAppService : ApplicationService, ICategoryAppService
{
    private readonly ICategoryRepository _categoryRepositroy;
    private readonly CategoryDomainService  _categoryDomainService;

    public CategoryAppService(ICategoryRepository categoryRepositroy
        , CategoryDomainService categoryDomainService
        )
    {
        _categoryRepositroy = categoryRepositroy;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<bool> AnyAsync()
    {
        var count = await _categoryRepositroy.GetCountAsync();
        return count > 0;
    }

    public async Task ImportAsync(IEnumerable<CategoryImportDto> importDtos)
    {
        var categories = ObjectMapper.Map<IEnumerable<CategoryImportDto>,
            IEnumerable<CategoryEntity>>(importDtos);

        await _categoryRepositroy.InsertManyAsync(categories);
    }

    public async Task<CategoryDto> GetAsync(long id)
    {
        var categoryDomain = await _categoryDomainService.GetAsync(id);
        var categoryDto = ObjectMapper.Map<CategoryEntity, CategoryDto>(categoryDomain.category);

        categoryDto.TopicTimes = categoryDomain.topicTimes;

        return categoryDto;
    }

    public async Task<ListResultDto<CategoryDto>> GetListAsync()
    {
        var categoryQueryable = await _categoryRepositroy.GetQueryableAsync();

        var parentCategoryList = await _categoryRepositroy.GetListAsync(c =>
            !c.ParentCategory.HasValue || c.ParentCategory == 0);
        var parentCategoryIdArray = parentCategoryList.Select(c => c.Id).ToList();
        var parentCategoryDtoList = ObjectMapper.Map<List<CategoryEntity>, List<CategoryDto>>(parentCategoryList);

        var stackIds = new ConcurrentStack<long>();
        parentCategoryIdArray.ForEach(parentCategoryId => stackIds.Push(parentCategoryId));
        while (stackIds.TryPop(out long categoryId))
        {
            var subCategories = categoryQueryable.Where(c => c.ParentCategory == categoryId).ToList();
            if (subCategories.Any())
            {
                var curCategoryDto = parentCategoryDtoList.Single(c => c.Id == categoryId);
                var subDtoCategories = ObjectMapper.Map<List<CategoryEntity>, List<CategoryDto>>(subCategories);
                curCategoryDto.SubCategories = subDtoCategories;

                var categoryIdArray =_categoryDomainService.GetSubCategoryIds(categoryQueryable, categoryId);
                var topicTimes = _categoryDomainService.GetTopicTimes(categoryIdArray);
                curCategoryDto.TopicTimes =await _categoryDomainService.GetTopicTimes(categoryIdArray); 

                foreach(var subCategory in subDtoCategories)
                {
                    var cid = subCategory.Id;
                    stackIds.Push(cid);

                    var subCategoryIdArray = _categoryDomainService.GetSubCategoryIds(categoryQueryable, cid);
                    subCategory.TopicTimes = await _categoryDomainService.GetTopicTimes(subCategoryIdArray); 
                }
            }
        }

        return new ListResultDto<CategoryDto>(parentCategoryDtoList);
    }

   
}