using System.Text.Json;
using Planday.Schedule.Dependencies.EmployeeClient.Models;
using Planday.Schedule.Infrastructure.Models;

namespace Planday.Schedule.Dependencies.EmployeeClient.Client;

public class HttpEmployeeClient: IHttpEmployeeClient
{
   private readonly HttpClient _httpClient;
   public HttpEmployeeClient(HttpClient httpClient)
   {
     _httpClient = httpClient; 
     _httpClient.BaseAddress = new Uri("http://planday-employee-api-techtest.westeurope.azurecontainer.io:5000/");
     AddAuthorizationHeaders();
   }

   private void AddAuthorizationHeaders()
   {
      _httpClient.DefaultRequestHeaders.Add("Authorization","8e0ac353-5ef1-4128-9687-fb9eb8647288");
   }

   public async Task<Employee> GetEmployee(int id)
   {
     var response = await _httpClient.GetAsync($"employee/{id}");  
     response.EnsureSuccessStatusCode();
     var content = await response.Content.ReadAsStringAsync();
     
     var employeeDto = JsonSerializer.Deserialize<EmployeeDto>(content) ?? 
                       throw new NullReferenceException("Unable to deserialize response");
     
     return employeeDto.ToEmployee();
   }
}