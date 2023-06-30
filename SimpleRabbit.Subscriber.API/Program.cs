using SimpleRabbit.Subscriber.DapperDataAccess;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRabbit.Core.Domain.Bus;
using SimpleRabbit.Infra.Ioc;
using SimpleRabbit.Subscriber.DapperDataAccess.Migrations;
using SimpleRabbit.Subscriber.Application.EventHandlers;
using SimpleRabbit.Subscriber.Application.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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

RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Rabbit-Subscriber v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
ConfigureEventBus(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}

void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<PersonCreatedEvent, PersonEventHandler>();
}