using System.Collections.Generic;

namespace UWorx.HR.Abstractions;

public interface IResult
{
    ICollection<string> Errors { get; }
    bool Succeeded { get; }
    IResult AddError(string error); // for fluent api
}

public interface IDataResult<T> : IResult
{
    T Data { get; }
}
