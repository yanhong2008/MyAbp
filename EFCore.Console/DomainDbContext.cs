using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Console
{
    public class DomainDbContext : DbContext
    {
 

        private ILoggerFactory _iLoggerFactory = null;
        private IConfiguration _configuration;

        public DomainDbContext(DbContextOptions options, ILoggerFactory iLoggerFactory, IConfiguration configuration) : base(options)
        {
            _iLoggerFactory = iLoggerFactory;
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var conn = configuration.GetConnectionString("Default");
            optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));



            //方式一：自定义的log
            //optionsBuilder.UseLoggerFactory(new CustomEFLoggerFactory());
            ////方式二：netcore的log
            //optionsBuilder.UseLoggerFactory(_iLoggerFactory);
        }

        public DbSet<CategoryEntity> CategoryEntities { get; set; }

        public DbSet<TopicEntity> TopicEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
    }
}
