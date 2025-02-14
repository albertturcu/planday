using Microsoft.Extensions.DependencyInjection;
using Planday.Schedule.Infrastructure.Models;
using Planday.Schedule.Infrastructure.Repositories;

namespace Planday.Schedule.Infrastructure;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddScheduleRepositories(this IServiceCollection services)
   {
      services.AddScoped<IShiftRepository, ShiftRepository>();
      services.AddScoped<IGenericRepository<Employee>, EmployeeRepository>();

      return services;
   } 
}