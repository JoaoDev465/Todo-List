using System.Text.Json.Serialization;

namespace TodoListCore.Response;

public class Responses<TData>
{
  
    [JsonConstructor]
    public Responses(TokenResponse data) {}

    public Responses(TData? data,
        int code,
        string? message = null)
    {
        Data = data;
        Code = code;
        Message = message;
    }


    public int Code { get; set; }
    public string? Message { get; set; }

    public TData? Data { get; set; }

    [JsonIgnore] public bool IsSuccess => Code is >= 200 and <= 299;
    [JsonIgnore] public bool IsError => Code is >= 400 and <= 599;
}