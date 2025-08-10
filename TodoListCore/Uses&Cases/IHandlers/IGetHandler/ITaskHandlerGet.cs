using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers.IGetHandler;

public interface ITaskHandlerGet
{
    Task<PageResponse<List<Todo>>> GetTaskListAsync(TodoDto request);
    Task<Responses<Todo?>> GetByIdAsync(TodoDto request);
}