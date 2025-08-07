using System.Text.Json.Serialization;
using TodoList.Proj.Models;

namespace TodoListCore.Response;

public class Responses<TData>
{
  
    [JsonConstructor]
    public Responses() {}

    public Responses(TData? data,
        int code = Configurations.DefaultStatusCode,
        string? message = null)
    {
        Data = data;
        Code = code;
        Message = message;
    }

    public static Responses<TData> Error(TData data,
        int code = Configurations.DefaultstatusError,
        string? message = null) => new Responses<TData>(default, code, message);

    public int Code { get; set; }
    public string? Message { get; set; }

    public TData? Data { get; set; }

    [JsonIgnore] public bool IsSuccess => Code is >= 200 and <= 299;
    [JsonIgnore] public bool IsError => Code is >= 400 and <= 499;
}