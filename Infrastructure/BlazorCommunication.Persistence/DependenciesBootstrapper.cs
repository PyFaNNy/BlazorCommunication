using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazorCommunication.Application.Interfaces;

namespace BlazorCommunication.Persistence;

public static class DependenciesBootstrapper
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlazorCommunicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetValue<string>("MSSQL_URL"),
                b => b.MigrationsAssembly(typeof(BlazorCommunicationDbContext).Assembly.FullName));

            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
        services.AddScoped<IBlazorCommunicationDbContext>(provider => provider.GetService<BlazorCommunicationDbContext>());

        return services;
    }
}