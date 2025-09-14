using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using SmsDemoApp.Application.Contracts.FileService.Interfaces;
using SmsDemoApp.Application.Messaging;
using SmsDemoApp.Infrastructure.CrossCutting.FileStorageService.Implementations;
using SmsDemoApp.Infrastructure.CrossCutting.Messaging;

namespace SmsDemoApp.Infrastructure.CrossCutting.FileStorageService.Extensions;

public static class FileStorageServiceCollectionExtensions
{
    public static IServiceCollection AddFileService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinio(client =>
        {
            client.WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
                .WithEndpoint(configuration["Minio:Endpoint"])
                .WithSSL(configuration.GetValue<bool>("Minio:UseSsl"));
        });

        services.AddScoped<IFileService, MinioStorageService>();
        services.AddScoped<IEventPublisher, MassTransitEventPublisher>();


        services.AddKeyedScoped<IMinioClient>("SasMinioClient", (serviceProvider,_) =>
        {
            var client = new MinioClient();
           return client.WithEndpoint(configuration["Minio:SasEndpoint"])
                .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
                .WithSSL(configuration.GetValue<bool>("Minio:UseSsl"));
        });

        return services;
    }

}