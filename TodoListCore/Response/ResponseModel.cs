using System.Text.Json.Serialization;

namespace TodoListCore.Response;

public class Responses<TData>
{
    private readonly int _code;

    [JsonConstructor]
    public Responses() => _code = Configurations.DefaultStatusCode;

    public Responses(TData? data,
        int code = Configurations.DefaultStatusCode,
        string? message = null)
    {
        Data = data;
        _code = code;
        Message = message;
    }

    public string? Message { get; set; }

    public TData? Data { get; set; }

    [JsonIgnore] public bool IsSuccess => _code is >= 200 and <= 299;
}