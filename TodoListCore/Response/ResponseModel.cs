using System.Text.Json.Serialization;

namespace TodoListCore.Response;

public class Responses<TData>
{
    private readonly int _code;

    [JsonConstructor]
    public Responses() {}

    public Responses(TData? data,
        int code = Configurations.DefaultStatusCode,
        string? message = null)
    {
        Data = data;
        _code = code;
        Message = message;
    }

    public static Responses<TData>Error(TData data,
        int code = Configurations.DefaultstatusError,
        string? message = null) => new Responses<TData>(default, code, message);

    public int Code { get; set; }
    public string? Message { get; set; }

    public TData? Data { get; set; }

    [JsonIgnore] public bool IsSuccess => _code is >= 200 and <= 299;
    [JsonIgnore] public bool IsError => _code is >= 400 and <= 499;
}