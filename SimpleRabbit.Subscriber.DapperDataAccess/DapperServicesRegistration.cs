using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleRabbit.Subscriber.DapperDataAccess.Repository;
using SimpleRabbit.Subscriber.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.DapperDataAccess
{
    public static class DapperServicesRegistration
    {
        public static IServiceCollection ConfigureDapperServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c.AddSqlServer2012()
              .WithGlobalConnectionString(configuration.GetConnectionString("AppConnString"))
              .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

            return services;
        }
    }
}
