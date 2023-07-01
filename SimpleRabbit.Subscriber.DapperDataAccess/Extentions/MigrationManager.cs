using FluentMigrator.Runner;
using SimpleRabbit.Subscriber.DapperDataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace SimpleRabbit.Subscriber.DapperDataAccess.Extentions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    databaseService.CreateDatabase("SimpleRabbitDB");

                    migrationService.ListMigrations();
                    migrationService.MigrateUp();

                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return host;
        }
    }
}
