﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Zhaoxi.Forum.Application;
using Zhaoxi.Forum.Application.Validator;
using Zhaoxi.Forum.EntityFrameworkCore;
using Zhaoxi.Forum.HttpApi.Host.Handlers;

namespace Zhaoxi.Forum.HttpApi.Host
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule), typeof(AbpAutofacModule),
        typeof(ForumApplicationModule),
        typeof(ForumEntityFrameworkCoreModule))]
    public class ForumHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;

            var configuration = services.GetConfiguration();
            //services.AddAuthorization();
            //services.AddAuthentication();
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
            //https://www.cnblogs.com/wukankan/p/14805565.html
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.TokenCookie.SameSite = SameSiteMode.Lax;
                options.TokenCookie.Expiration = TimeSpan.FromDays(365);
                options.AutoValidateIgnoredHttpMethods.Add("POST");
                options.AutoValidateIgnoredHttpMethods.Add("PUT");
                options.AutoValidateIgnoredHttpMethods.Add("DELETE");
            });


            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = configuration["Redis:ConnectionString"];
            });

            // Configure Jwt Authentication
            ConfigureAuthentication(context);


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
            var env = context.GetEnvironment();
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
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseConfiguredEndpoints();
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var configuration = services.GetConfiguration();
            string issuer = configuration["Jwt:Issuer"];
            string audience = configuration["Jwt:Audience"];
            string expire = configuration["Jwt:ExpireMinutes"];
            TimeSpan expiration = TimeSpan.FromMinutes(Convert.ToDouble(expire));
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]));

            services.AddAuthorization(options =>
            {
                // 1、Definition authorization policy
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PolicyRequirement()));
            }).AddAuthentication(s =>
            {
                // 2、Authentication
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(s =>
            {
                // 3、Use Jwt bearer 
                s.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = key,
                    ClockSkew = expiration,
                    ValidateLifetime = true
                };
                s.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // DI handler process function
            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();
        }
    }
}
