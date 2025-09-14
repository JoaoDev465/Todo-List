using System.Collections.Concurrent;
using System.Security.AccessControl;

namespace TodoListCore;

public class PageRequest
{
    public int PageSize { get; set; } = Configurations.defaultpagesize;

    public int PageNumber { get; set; } = Configurations.defaultpagenumber;
}