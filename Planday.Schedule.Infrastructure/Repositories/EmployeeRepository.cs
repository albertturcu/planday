using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.Models;
using Planday.Schedule.Infrastructure.Providers.Interfaces;

namespace Planday.Schedule.Infrastructure.Repositories;

public class EmployeeRepository: IGenericRepository<Employee>
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public EmployeeRepository(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public async Task<Employee?> GetAsync(int id)
    {
        await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

        string query = @"SELECT * FROM Employee WHERE Id = @EmployeeId;";
        return await sqlConnection.QueryFirstOrDefaultAsync<Employee>(query, new { EmployeeId = id });
    }
    
    public Task<IReadOnlyCollection<Employee>> GetAllAsync() => throw new NotImplementedException();

    public Task<int> AddAsync(Employee entity) => throw new NotImplementedException();
    
    public Task UpdateAsync(Employee entity) => throw new NotImplementedException();
}