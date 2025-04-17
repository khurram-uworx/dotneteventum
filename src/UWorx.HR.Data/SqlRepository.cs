using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UWorx.HR.Abstractions;
using UWorx.HR.Data.Helpers;
using UWorx.HR.Implementations;
using UWorx.HR.Repositories;

namespace UWorx.HR.Data;

public class SqlRepository : IHRRepository
{
    readonly string connectionString;

    public SqlRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IDataResult<HREmployeeInfo> GetEmployeeByEmail(string email)
    {
        var sql = @"select u.UserIndex, u.UserEmail,
                e.EmployeeIndex, e.FirstName, e.MiddleName, e.LastName
            from users u join employeeusers eu on u.userindex = eu.userindex
                join hr_employees e on eu.employeeindex = e.employeeindex
            where u.useremail = @email";
        var p = new SqlParameter[]
        {
            new SqlParameter("@email", email)
        };

        var reader = SqlHelper.ExecuteReader(this.connectionString, CommandType.Text, sql, p);
        var list = new List<HREmployeeInfo>();

        while (reader.Read())
        {
            list.Add(new HREmployeeInfo()
            {
                UserGuid = Guid.NewGuid(),
                UserIndex = SqlReaderHelper.GetValue<int>(reader, "UserIndex"),
                UserEmail = SqlReaderHelper.GetValue<string>(reader, "UserEmail"),

                EmployeeIndex = SqlReaderHelper.GetValue<int>(reader, "EmployeeIndex"),
                FirstName = SqlReaderHelper.GetValue<string>(reader, "FirstName"),
                MiddleName = SqlReaderHelper.GetValue<string>(reader, "MiddleName"),
                LastName = SqlReaderHelper.GetValue<string>(reader, "LastName")
            });
        }

        return new DataResult<HREmployeeInfo>(list.Count > 0 ? list[0] : null);
    }

    public bool PunchIn(string email, bool workingFromHome)
    {
        return false;
    }

    public bool PunchOut(string email)
    {
        return false;
    }
}
