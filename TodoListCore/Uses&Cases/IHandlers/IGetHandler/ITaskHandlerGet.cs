using TodoList.Proj.Models;
using TodoListCore.DTO;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.IHandlers.IGetHandler;

public interface ITaskHandlerGet
{
    Task<PageResponse<Todo>> GetTaskListAsync(TodoDto request);
    Task<Responses<Todo?>> GetByIdAsync(TodoDto request);
}