using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskList.Application.Abstract;
using TaskList.Application.AutoMapperProfiles;
using TaskList.Application.CommandHandlers;
using TaskList.Application.ReaderLogics;
using TaskList.Domain;
using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Domain.UnitOfWorks.UnitOfWorkForSql;
using TaskList.Shared.Common.Extensions;

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

        var connectionString = Configuration.TryGetEnvironmentSetting("Postgres1");
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<DbContext, SqlContext>(optionsAction => optionsAction.UseNpgsql(connectionString));

        services.AddControllers();
        
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
