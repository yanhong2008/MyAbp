using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Zhaoxi.Forum.Application;
using Zhaoxi.Forum.EntityFrameworkCore;

namespace Zhaoxi.Forum.HttpApi.Host
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule),typeof(AbpAutofacModule),
        typeof(ForumApplicationModule),
        typeof(ForumEntityFrameworkCoreModule))]
    public class ForumHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var configuration = services.GetConfiguration();

            context.Services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("AllowAll", corsPolicy =>
                {
                    corsPolicy.AllowAnyHeader()
                              .AllowAnyOrigin()
                              .AllowAnyMethod();
                });
            });

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ForumApplicationModule).Assembly);
            });

            services.AddSwaggerGen(option =>
            {
 
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "YH.ABP",
                    Version = "v1",
                });
                //option.IncludeXmlComments()
                option.DocInclusionPredicate((docName, predicate) => true);
                option.CustomSchemaIds(type => type.FullName);

                //生成的xml文档位置
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "Swagger.xml");
                option.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                ////实体的xml文档位置
                //option.IncludeXmlComments(Path.Combine(basePath, "Depin.Nursing.Manage.Model.xml"), true);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env=context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "YH.ABP");
            });

            app.UseRouting();
            app.UseConfiguredEndpoints();
        }
    }
}
