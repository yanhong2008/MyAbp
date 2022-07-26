using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Zhaoxi.Forum.DbMigrator
{
    public class ForumDbMigrationService : ITransientDependency
    {
        public ILogger<ForumDbMigrationService> Logger { get; set; }

        public ForumDbMigrationService()
        {
            Logger = NullLogger<ForumDbMigrationService>.Instance;
        }

        public void Migrate()
        {
            var initialMigrationAdded = AddInitialMigrationIfNotExist();
            if (initialMigrationAdded)
            {
                return;
            }
            else
            {
                UpdateInitialMigrator();
            }
        }

        private bool AddInitialMigrationIfNotExist()
        {
            try
            {
                if (!MigrationsFolderExists())
                {
                    AddInitalMigration();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning("不能决定是否已有迁移文件存在:" + ex.Message);
                return false;
            }
        }

        private bool MigrationsFolderExists()
        {
            var dbMigrationsProjectFolder = GetDbMigratorProjectFolderPath();
            return Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
        }


        private void AddInitalMigration()
        {
            Logger.LogInformation("开始创建迁移文件");

            string argumentPrefix;
            string fileName;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux)){
                argumentPrefix = "c";
                fileName = "/bin/bash";
            }
            else
            {
                argumentPrefix = "/C";
                fileName = "cmd.exe";
            }

            var processStartInfo = new ProcessStartInfo(fileName, $"{argumentPrefix} dotnet ef migrations add InitialCreate");
            processStartInfo.WorkingDirectory = GetDbMigratorProjectFolderPath();
            try
            {
                Process.Start(processStartInfo);
            }
            catch(Exception ex)
            {
                Logger.LogError($"开始创建迁移文件:", ex.Message);
                throw new Exception("不能dotnet ef migrations");
            }
        }

        private string GetDbMigratorProjectFolderPath()
        {
            var slnDirectoryPath=GetSolutionDirectoryPath();
            if(slnDirectoryPath == null)
            {
                throw new Exception("Solution folder not found!");
            }
            var srcDirectoryPath = Path.Combine(slnDirectoryPath);
            return Directory.GetDirectories(srcDirectoryPath).FirstOrDefault(p => p.EndsWith(".DbMigrator"));
        }

        private string GetSolutionDirectoryPath()
        {
            var currentDirectory=new DirectoryInfo(Directory.GetCurrentDirectory());

            while (Directory.GetParent(currentDirectory.FullName) != null)
            {
                currentDirectory = Directory.GetParent(currentDirectory.FullName);
                if (Directory.GetFiles(currentDirectory!.FullName).FirstOrDefault(f => f.EndsWith(".sln")) != null)
                {
                    return currentDirectory.FullName;
                }
            }
            return null;
        }

    
        private void UpdateInitialMigrator()
        {
            Logger.LogInformation("Update initial migration...");

            string argumentPrefix;
            string fileName;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                argumentPrefix = "-c";
                fileName = "/bin/bash";
            }
            else
            {
                argumentPrefix = "/C";
                fileName = "cmd.exe";
            }


            var procStartInfo = new ProcessStartInfo(fileName,
                $"{argumentPrefix} dotnet ef database update"
            );
            procStartInfo.WorkingDirectory = GetDbMigratorProjectFolderPath();

            try
            {
                Process.Start(procStartInfo);
            }
            catch (Exception ex)
            {
                Logger.LogError("Update initial migration...", ex.Message);
                throw new Exception("Couldn't run dotnet ef migrations...");
            }
        }


    }
}
