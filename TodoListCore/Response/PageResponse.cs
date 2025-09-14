using System.Text.Json.Serialization;

namespace TodoListCore.Response;

public class PageResponse<TData>: Responses<TData>
{
    [JsonConstructor]
   public  PageResponse(
        TData? data,
        int totalcount,
        int code,
        int currentCount = 1,
        int pageSize = Configurations.defaultpagesize)
        :base(data,code)
   {
       Code = code;
        Data = data;
        TotalCount = totalcount;
        CurrentCount = currentCount;
        PageSize = pageSize;
   }

    public PageResponse
    (
        TData? data,
        int code,
        string? message = null
        ):base(data,code,message)
    {
    }

 

    public int PageSize { get; set; } = Configurations.defaultpagesize;
    
    public int Totalpages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int CurrentCount { get; set; }

    public int TotalCount { get; set; }
}