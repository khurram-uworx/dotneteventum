using System;
using UWorx.HR.Abstractions;

namespace UWorx.HR.Repositories;

public class HREmployeeInfo
{
    public int UserIndex { get; set; }
    public Guid UserGuid { get; set; }
    public int EmployeeIndex { get; set; }
    public string UserEmail { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
}

public interface IHRRepository
{
    IDataResult<HREmployeeInfo> GetEmployeeByEmail(string email);
    bool PunchIn(string email, bool workingFromHome);
    bool PunchOut(string email);
}
