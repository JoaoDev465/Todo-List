using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace TodoListCore.Response;

public class ErrorResponses<TData>
{
    private  readonly int _code;
    
    [JsonConstructor]
    public ErrorResponses() => _code = Configurations.DefaultstatusError;

    public ErrorResponses(TData? data,
        int code = Configurations.DefaultstatusError,
        string? message = null
    )
    {
        Data = data;
        Message = message;
        _code = code;
    }
    public int Code { get; set; }
    public string? Message { get; set; }
    public TData? Data { get; set; }

    [JsonIgnore] public bool IsError => _code is >= 400 and <= 499;
}