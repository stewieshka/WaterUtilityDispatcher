using System.ComponentModel;
using WaterUtilityDispatcher.Domain.BrigadeRoot;
using WaterUtilityDispatcher.Domain.Common;

namespace WaterUtilityDispatcher.Domain.WorkerRoot;
public sealed class Worker : EntityBase
{
    public Worker(Guid id,
        string firstName, string lastName, string middleName,
        DateTime birthDay, DateTime employmentDate, decimal salary) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        BirthDay = birthDay;
        EmploymentDate = employmentDate;
        Salary = salary;
    }

    public Worker()
    {
        
    }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime EmploymentDate { get; set; }
    public decimal Salary { get; set; }
    [Browsable(false)]
    public Brigade? Brigade { get; set; }
    public string? BrigadeName => Brigade?.Name;
}