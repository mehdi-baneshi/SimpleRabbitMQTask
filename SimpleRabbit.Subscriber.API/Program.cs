using Microsoft.OpenApi.Models;
using Serilog;
using SimpleRabbit.Infra.Ioc;
using SimpleRabbit.Subscriber.API.Middleware;
using SimpleRabbit.Subscriber.DapperDataAccess;
using SimpleRabbit.Subscriber.DapperDataAccess.Extentions;
using SimpleRabbit.Subscriber.RedisDataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDapperServices(builder.Configuration);
builder.Services.ConfigureRedisServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple Rabbit-Subscriber", Version = "v1" });
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlerMidlleware>();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Rabbit-Subscriber v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    app.MigrateDatabase();
}
catch (Exception ex)
{
    app.Services.GetRequiredService<ILogger<Program>>().LogError(ex, ex.Message);
}

app.Run();

void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}