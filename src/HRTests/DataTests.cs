using System;
using System.Data.SqlClient;
using System.Diagnostics;
using UWorx.HR.Data;
using UWorx.HR.Repositories;

namespace HRTests;

public class DataTests
{
    const string connectionString = "Server=.;Database=HumanResources;Integrated Security=True;";
    //TrustServerCertificate=True;Application Name=
    string testUser = "test@email.com";

    int checkRow(SqlConnection connection, string sql, SqlParameter parameter)
    {
        var commandSelect = new SqlCommand(sql, connection);
        commandSelect.Parameters.Add(parameter);
        return Convert.ToInt32(commandSelect.ExecuteScalar()); // if row is missing we will get null convert will make it 0
    }

    [SetUp]
    public void InitializeDatabase()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Debug.WriteLine("Connection successful!");

                string sqlUser = "select userindex from users where useremail = @email";
                var pUser = new SqlParameter("@email", this.testUser);
                int userIndex = checkRow(connection, sqlUser, pUser);
                if (userIndex <= 0)                                             
                {
                    var commandInsert = new SqlCommand("insert into users (useremail) values (@email)", connection);
                    commandInsert.Parameters.Add(new SqlParameter("@email", this.testUser));
                    int affectedRows = commandInsert.ExecuteNonQuery();
                    userIndex = checkRow(connection, sqlUser, pUser);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }
    }

    [Test]
    public void SqlTestScenario()
    {
        IHRRepository repository = new SqlRepository(connectionString);
        var result = repository.GetEmployeeByEmail(this.testUser);
        Assert.IsTrue(result.Succeeded);
        Assert.IsNotNull(result.Data);
    }
}
