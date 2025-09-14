using MassTransit;
using SmsDemoApp.Infrastructure.Identity.Extensions;
using SmsDemoApp.Worker;
using SmsDemoApp.Application.Extensions;
using SmsDemoApp.Infrastructure.Persistence.Extensions;

var builder = Host.CreateApplicationBuilder(args);

// Worker اصلی
builder.Services.AddHostedService<Worker>();

// MassTransit
builder.Services.AddMassTransit(x =>
{
    // Consumer خودت
    //x.AddConsumer<SmsCreatedConsumer>();
    builder.Services
    .AddIdentityServices(builder.Configuration)
    .AddApplicationAutomapper()
    .AddApplicationMediatorServices()
    .RegisterApplicationValidators()
    .AddPersistenceDbContext(builder.Configuration);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"]);
            h.Password(builder.Configuration["RabbitMq:Password"]);
        });
        
        
        cfg.ReceiveEndpoint("sms-created-queue", e =>
        {
            e.ConfigureConsumer<SmsCreatedConsumer>(context);
        });
    });
});

// Build و Run
var host = builder.Build();
host.Run();
