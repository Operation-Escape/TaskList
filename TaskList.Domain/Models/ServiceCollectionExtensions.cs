using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Contexts;
using TaskList.Domain.Contexts.Abstract;

namespace TaskList.Domain.Models
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoSettings(this IServiceCollection services, IConfiguration configuration)
        {
            string cs = configuration["MongoSettings:Connection"];
            string dbName = configuration["MongoSettings:DatabaseName"];

            services.Configure<MongoSettings>(options =>
            {
                options.ConnectionString = cs;
                options.Database = dbName;
            });
            
            services.AddScoped<IMongoContext, MongoContext>();

            return services;
        }
    }
}
