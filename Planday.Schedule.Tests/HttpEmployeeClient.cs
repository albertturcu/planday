using Microsoft.Extensions.DependencyInjection;
using Planday.Schedule.Dependencies.EmployeeClient;
using Planday.Schedule.Dependencies.EmployeeClient.Models;
using Xunit;

namespace Planday.Schedule.Tests;

public class HttpEmployeeClient
{
   [Fact]
    public static async Task Integration_GetEmployee_ShouldReturnCorrectEmployee()
    {
        // Arrange
        var expectedEmployeeDto = new EmployeeDto
        {
            Name = "John Doe",
            Email = "john@doe.com"
        };
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHttpEmployeeClient();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var _httpEmployeeClient = serviceProvider.GetRequiredService<Dependencies.EmployeeClient.Client.HttpEmployeeClient>();
        // Act
        var employee = await _httpEmployeeClient.GetEmployee(1);

        // Assert
        Assert.NotNull(employee);
        Assert.Equal(expectedEmployeeDto.Name, employee.Name);
        Assert.Equal(expectedEmployeeDto.Email, employee.Email);
    }

}