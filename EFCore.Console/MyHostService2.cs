using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Console
{
    public class MyHostService2 : IHostedService
    {
        private IHostApplicationLifetime _applicationLifetime;
        private IConfiguration _configuration;
        private DomainDbContext _domainDbContext;
        private IServiceProvider _serviceProvider;

        public MyHostService2(IHostApplicationLifetime applicationLifetime, IConfiguration configuration, DomainDbContext domainDbContext,
            IServiceProvider serviceProvider)
        {
            _applicationLifetime = applicationLifetime;
            _configuration = configuration;
            _domainDbContext = domainDbContext;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //_domainDbContext.UserEntities.Add(new UserEntity { NickName = "tom2", Email = "yan@qq.com2", Sex = 1 });
            //_domainDbContext.SaveChanges();

            var data=_domainDbContext.Set<UserEntity>().AsQueryable().ToList();
            var data2 = _domainDbContext.UserEntities.ToList();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
    
         
}
