using Microsoft.EntityFrameworkCore;
using TaskList.Application.Abstract;
using TaskList.Application.AutoMapperProfiles;
using TaskList.Application.CommandHandlers;
using TaskList.Application.ReaderLogics;
using TaskList.Domain.Contexts;
using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.Repositories;
using TaskList.Domain.Repositories.TaskRepositories;
using TaskList.Domain.UnitOfWorks;
using TaskList.Shared.Common.Extensions;
using Microsoft.OpenApi.Models;
using TaskList.Api.Middleware;
using TaskList.Shared.Common.Swagger;

namespace TaskList.Api;

public class Startup {
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddControllers();
        
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
        
        var swaggerConfig = ConfigModel.Bind(Configuration);
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc(swaggerConfig.SwaggerOptions.Version, new OpenApiInfo { Title = swaggerConfig.SwaggerOptions.Title, Version = swaggerConfig.SwaggerOptions.Version });
        });
        
        services.AddAutoMapper(typeof(TaskAutoMapperProfile));
        
        var connectionString = Configuration.TryGetEnvironmentSetting("Postgres");
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<DbContext, SqlContext>(optionsAction => optionsAction.UseNpgsql(connectionString));

        services.AddScoped<IMongoContext, MongoContext>();
        
        services.AddScoped(typeof(MongoRepository<,>));
        services.AddScoped(typeof(SqlRepository<,>));
        
        services.AddScoped(typeof(MongoRepository<,>));
        services.AddScoped(typeof(SqlRepository<,>));

        services.AddScoped<MongoTaskRepository>();
        services.AddScoped<SqlTaskRepository>();

        services.AddScoped<MongoUnitOfWork>();
        services.AddScoped<SqlUnitOfWork>();

        services.AddScoped<ITaskReaderLogic, TaskReaderLogic>();
        services.AddScoped<ITaskCommandHandler, TaskCommandHandler>();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env) {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        using (var scope = app.Services.CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<SqlContext>();
            dataContext.Database.Migrate();
        }
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
