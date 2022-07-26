using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Domain;

namespace Zhaoxi.Forum.Application;

[DependsOn(typeof(AbpAutoMapperModule),
    typeof(ForumApplicationContractsModule),
    typeof(ForumDomainModule))]
public class ForumApplicationModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;

        // 添加ObjectMapper注入
        services.AddAutoMapperObjectMapper<ForumApplicationModule>();

        // Abp AutoMapper设置
        Configure<AbpAutoMapperOptions>(config =>
        {
            config.AddProfile<ForumApplicationAutoMapperProfile>();
        });
    }
}
