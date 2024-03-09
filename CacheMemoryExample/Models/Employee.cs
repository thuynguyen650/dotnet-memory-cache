using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CacheMemoryExample.Models;

public class Employee
{
    [Key] // unique
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // generate when inserted
    public long EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

}
