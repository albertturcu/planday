using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.Models;
using Planday.Schedule.Infrastructure.Providers.Interfaces;

namespace Planday.Schedule.Infrastructure.Repositories;

public interface IShiftRepository: IGenericRepository<Shift>
{
    Task<Shift?> GetByEmployeeIdAsync(int employeeId, DateTime startTime, DateTime endTime);
}

public class ShiftRepository : IShiftRepository 
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public ShiftRepository(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public async Task<Shift?> GetByEmployeeIdAsync(int employeeId, DateTime startTime, DateTime endTime)
    {
        await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());
        string query = @"
                SELECT * 
                FROM Shift 
                WHERE EmployeeId = @EmployeeId 
                AND (
                    (@StartTime BETWEEN Start AND End) 
                    OR (@EndTime BETWEEN Start AND End) 
                    OR (Start BETWEEN @StartTime AND @EndTime)
                );";

        return await sqlConnection.ExecuteScalarAsync<Shift>(query, new { EmployeeId = employeeId, StartTime = startTime, EndTime = endTime });
    }
    
    public async Task<Shift?> GetAsync(int id)
    {
        await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());
        
        string query = @"SELECT * FROM Shift WHERE Id = @ShiftId;";
        return await sqlConnection.QueryFirstOrDefaultAsync<Shift>(query, new { ShiftId = id });
    } 
    
    public async Task<IReadOnlyCollection<Shift>> GetAllAsync()
    {
        await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

        string query = @"SELECT Id, EmployeeId, Start, End FROM Shift;";
        return (await sqlConnection.QueryAsync<Shift>(query)).ToList();
    }
    
    public async Task<int> AddAsync(Shift shift)
    {
       await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

       string query = @"
           INSERT INTO Shift (EmployeeId, Start, End)
           VALUES (NULL, @StartTime, @EndTime);
           SELECT last_insert_rowid();";
     
       int shiftId = await sqlConnection.ExecuteScalarAsync<int>(query, new
       {
           StartTime = shift.Start.ToInvariantFormat(DateTimeExtensions.SqlDateTimeFormat),
           EndTime = shift.End.ToInvariantFormat(DateTimeExtensions.SqlDateTimeFormat),
       });

       return shiftId;
    }
    
    public async Task UpdateAsync(Shift shift)
    {
        await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

        // Dynamically build the UPDATE query
        var updateFields = new List<string>();
        var parameters = new DynamicParameters();

        if (shift.EmployeeId.HasValue)
        {
            updateFields.Add("EmployeeId = @EmployeeId");
            parameters.Add("EmployeeId", shift.EmployeeId.Value);
        }
    
        if (shift.Start != default)
        {
            updateFields.Add("Start = @StartTime");
            parameters.Add("StartTime", shift.Start.ToInvariantFormat(DateTimeExtensions.SqlDateTimeFormat));
        }

        if (shift.End != default)
        {
            updateFields.Add("End = @EndTime");
            parameters.Add("EndTime", shift.End.ToInvariantFormat(DateTimeExtensions.SqlDateTimeFormat));
        }

        if (!updateFields.Any())
        {
            throw new ArgumentException("No fields provided for update.");
        }

        parameters.Add("ShiftId", shift.Id);
    
        string query = $"UPDATE Shift SET {string.Join(", ", updateFields)} WHERE Id = @ShiftId;";

        await sqlConnection.ExecuteAsync(query, parameters);
    }
}