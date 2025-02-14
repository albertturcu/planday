using Microsoft.Extensions.DependencyInjection;
using Planday.Schedule.Dependencies.EmployeeClient.Client;

namespace Planday.Schedule.Dependencies.EmployeeClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpEmployeeClient(this IServiceCollection services)
    {
        services.AddHttpClient<HttpEmployeeClient>();
        services.AddScoped<HttpEmployeeClient>();
        
        return services;
    }
}