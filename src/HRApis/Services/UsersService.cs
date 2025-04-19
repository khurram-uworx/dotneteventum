using System;
using UWorx.HR.Abstractions;
using UWorx.HR.Implementations;
using UWorx.HR.Repositories;

namespace HRApis.Services;

public class UserService : IUsersService
{
    readonly IHRRepository repository;

    public UserService(IHRRepository repository)
    {
        this.repository = repository;
    }

    public IResult GetUserInformationByEmail(string email)
    {
        try
        {
            var result = this.repository.GetEmployeeByEmail(email);
            return result;
        }
        catch (Exception ex)
        {
            return new Result().AddError(ex.Message);
        }
    }
}
