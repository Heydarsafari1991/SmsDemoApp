using SmsDemoApp.Application.Contracts.FileService.Interfaces;
using SmsDemoApp.Application.Extensions;
using SmsDemoApp.Infrastructure.CrossCutting.FileStorageService.Extensions;
using SmsDemoApp.Infrastructure.CrossCutting.Logging;
using SmsDemoApp.Infrastructure.Identity.Extensions;
using SmsDemoApp.Infrastructure.Persistence.Extensions;
using SmsDemoApp.WebFramework.Extensions;
using SmsDemoApp.WebFramework.Filters;
using SmsDemoApp.WebFramework.Models;
using SmsDemoApp.WebFramework.Swagger;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using MassTransit;
using SmsDemoApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(LoggingConfiguration.ConfigureLogger);
// Add services to the container.

builder
    .AddSwagger("v1")
    .AddVersioning()
   ;

builder.Services
    .AddIdentityServices(builder.Configuration)
    .AddFileService(builder.Configuration)
    .AddApplicationAutomapper()
    .AddApplicationMediatorServices()
    .RegisterApplicationValidators()
    .AddPersistenceDbContext(builder.Configuration);

builder.ConfigureAuthenticationAndAuthorization();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(OkResultAttribute));
    options.Filters.Add(typeof(NotFoundAttribute));
    options.Filters.Add(typeof(ModelStateValidationAttribute));
    options.Filters.Add(typeof(BadRequestAttribute));
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult<Dictionary<string, List<string>>>),
        StatusCodes.Status400BadRequest));
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
        StatusCodes.Status401Unauthorized));
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
        StatusCodes.Status403Forbidden));
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
        StatusCodes.Status500InternalServerError));

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
});
builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<SmsDemoAppDbContext>(o =>
    {
        o.UseSqlServer();
        o.QueryDelay = TimeSpan.FromSeconds(1);
        o.DuplicateDetectionWindow = TimeSpan.FromMinutes(5);
        o.UseBusOutbox();
    });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"]);
            h.Password(builder.Configuration["RabbitMq:Password"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    await app.ApplyMigrationsAsync();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();