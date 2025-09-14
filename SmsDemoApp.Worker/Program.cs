using MassTransit;
using SmsDemoApp.Infrastructure.Identity.Extensions;
using SmsDemoApp.Worker;
using SmsDemoApp.Application.Extensions;
using SmsDemoApp.Infrastructure.Persistence.Extensions;
using SmsDemoApp.Worker.Consumer;
using SmsDemoApp.Infrastructure.CrossCutting.FileStorageService.Extensions;

var builder = Host.CreateApplicationBuilder(args);

// Worker اصلی
builder.Services.AddHostedService<Worker>();
builder.Services
    .AddIdentityServices(builder.Configuration)
    .AddFileService(builder.Configuration)
    .AddApplicationAutomapper()
    .AddApplicationMediatorServices()
    .RegisterApplicationValidators()
    .AddPersistenceDbContext(builder.Configuration);
// MassTransit
builder.Services.AddMassTransit(x =>
{
    
    x.AddConsumer<SmsCreatedConsumer>();
    

    x.UsingRabbitMq((context, cfg) =>
    {
       
        
        cfg.ReceiveEndpoint("sms-created-queue", e =>
        {
            e.ConfigureConsumer<SmsCreatedConsumer>(context);
        });
    });
   
});

// Build و Run
var host = builder.Build();
host.Run();
