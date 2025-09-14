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
        

        return services;
    }
}