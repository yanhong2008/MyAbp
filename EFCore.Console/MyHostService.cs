using Domain;
using Magicodes.ExporterAndImporter.Core;
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
    public class MyHostService : IHostedService
    {
        private IHostApplicationLifetime _applicationLifetime;
        private IConfiguration _configuration;
        private DomainDbContext _domainDbContext;
        private IServiceProvider _serviceProvider;
        private IImporter _importer;

        public MyHostService(IHostApplicationLifetime applicationLifetime, IConfiguration configuration, DomainDbContext domainDbContext,
            IServiceProvider serviceProvider, IImporter importer)
        {
            _applicationLifetime = applicationLifetime;
            _configuration = configuration;
            _domainDbContext = domainDbContext;
            _serviceProvider = serviceProvider;
            _importer = importer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ImportUserAsync();
        }

        private async Task ImportUserAsync()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "user.xlsx");
            var import = _importer.Import<UserImport>(filePath);
            if (import.IsCompleted && import.Result != null)
            {
                var importData = import.Result.Data;
                var users = new List<UserEntity>();
                foreach (var line in importData)
                {
                    var entity = new UserEntity
                    {
                        Phone = line.Phone,
                        Password = line.Password,
                        NickName = line.NickName,
                        Email = line.Email,
                        UserAvatar = line.UserAvatar,
                        Sex = line.Sex == "女" ? 0 : 1
                    };
                    users.Add(entity);
                }
                await _domainDbContext.AddRangeAsync(users);
                await _domainDbContext.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
           return  Task.CompletedTask;
        }

    }


}
