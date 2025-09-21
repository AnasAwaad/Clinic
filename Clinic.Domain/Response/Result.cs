using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Response;
public class Result<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public Result(bool succeeded,string message="",T? data = default)
    {
        Succeeded = succeeded;
        Message = message;
        Data = data;
    }

    // Optional static helpers
    public static Result<T> Success(T data, string message = "") => new(true, message, data);
    public static Result<T> Fail(string message) => new(false, message);
}
