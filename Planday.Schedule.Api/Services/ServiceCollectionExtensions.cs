using Planday.Schedule.Dependencies.EmployeeClient;
using Planday.Schedule.Infrastructure;

namespace Planday.Schedule.Api.Services;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddScheduleServices(this IServiceCollection services)
   {
      services.AddScheduleRepositories();
      services.AddHttpEmployeeClient();
      
      services.AddScoped<ShiftService>();
      services.AddScoped<EmployeeService>();
      
      return services;
   } 
}