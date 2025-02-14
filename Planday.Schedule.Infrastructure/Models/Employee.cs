namespace Planday.Schedule.Infrastructure.Models;
    
public record Employee
{
    public Employee(int id, string name, string email)
    {
       Id = id; 
       Name = name;
       Email = email;
    }
    
    public Employee(string name, string email)
    {
       Name = name;
       Email = email;
    }
    
    public Employee()
    {
    }
    
    public long Id { get; set; }
    public string Name { get; set;}
    public string Email { get; set;}
}    