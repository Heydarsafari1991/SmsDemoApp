using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Infrastructure.Persistence.Repositories.Common;
using MassTransit.EntityFrameworkCoreIntegration;

namespace SmsDemoApp.Infrastructure.Persistence.Extensions;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SmsDemoAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("db"), builder =>
            {
                builder.EnableRetryOnFailure();
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMassTransit(x =>
        {
            x.AddEntityFrameworkOutbox<SmsDemoAppDbContext>(o =>
            {
                o.UseSqlServer();
                o.QueryDelay = TimeSpan.FromSeconds(1);
                o.DuplicateDetectionWindow = TimeSpan.FromMinutes(5);
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], h =>
                {
                    h.Username(configuration["RabbitMq:Username"]);
                    h.Password(configuration["RabbitMq:Password"]);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}