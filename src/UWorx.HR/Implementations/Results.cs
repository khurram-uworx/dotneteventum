using System.Collections.Generic;
using UWorx.HR.Abstractions;

namespace UWorx.HR.Implementations;

public class Result : IResult
{
    readonly ICollection<string> errors;

    public Result()
    {
        this.errors = new List<string>();
    }

    public ICollection<string> Errors => this.errors;

    public bool Succeeded => Errors.Count == 0;

    public IResult AddError(string error) // for fluent api
    {
        this.errors.Add(error);
        return this;
    }
}

public class DataResult<T> : Result, IDataResult<T>
{
    readonly T data;

    public DataResult(T data) : base()
    {
        this.data = data;
        if (data is null)
            base.AddError("data missing");
    }

    public T Data => this.data;
}
