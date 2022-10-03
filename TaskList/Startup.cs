using Microsoft.EntityFrameworkCore;
using TaskList.Application.Abstract;
using TaskList.Application.AutoMapperProfiles;
using TaskList.Application.CommandHandlers;
using TaskList.Application.ReaderLogics;
using TaskList.Domain.Contexts;
using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.Repositories;
using TaskList.Domain.Repositories.Abstract;
using TaskList.Domain.Repositories.TaskRepositories;
using TaskList.Domain.UnitOfWorks;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Shared.Common.Extensions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TaskList.Api;

public class Startup {
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
        services.AddCors(o => o.AddPolicy("TaskListCors", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        builder.AddEnvironmentVariables();

        var connectionString = Configuration.TryGetEnvironmentSetting("Postgres");
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<DbContext, SqlContext>(optionsAction => optionsAction.UseNpgsql(connectionString));

        services.AddScoped<IMongoContext, MongoContext>();

        services.AddControllers();
        
        services.AddScoped(typeof(IRepository<,>), typeof(MongoRepository<,>));
        services.AddScoped(typeof(IRepository<,>), typeof(SqlRepository<,>));
        
        services.AddScoped<ITaskRepository, MongoTaskRepository>();
        services.AddScoped<ITaskRepository, SqlTaskRepository>();
        
        services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
        services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
        
        //var swaggerOptions = System.Text.Json.JsonDocument.Parse(Configuration.TryGetEnvironmentSetting("SwaggerOptions")).RootElement;
        //    //.GetProperty("id");
        //    var v = swaggerOptions.GetProperty("Version");
        //services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc(Configuration.GetSection(swaggerOptions.GetProperty("Version").ToString()).Value, new OpenApiInfo
        //    {
        //        Version = Configuration.GetSection(swaggerOptions.GetProperty("Version").ToString()).Value,
        //        Title = Configuration.GetSection(swaggerOptions.GetProperty("Title").ToString()).Value,
        //        Description = Configuration.GetSection(swaggerOptions.GetProperty("Description").ToString()).Value
        //    });
        //});

        services.AddAutoMapper(typeof(TaskAutoMapperProfile));
        services.AddScoped<ITaskReaderLogic, TaskReaderLogic>();
        services.AddScoped<ITaskCommandHandler, TaskCommandHandler>();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
