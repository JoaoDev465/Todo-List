using Microsoft.AspNetCore.Mvc;

namespace TodoList.Proj.DTOview;

public class QueryPagination
{
    [FromQuery(Name = "page")] public int Page { get; set; } = 1;

    [FromQuery(Name = "pageSize")] public int PageSize { get; set; } = 10;
}