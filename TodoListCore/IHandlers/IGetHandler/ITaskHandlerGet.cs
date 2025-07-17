using TodoList.Proj.Models;
using TodoListCore.DTO;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoListCore.IHandlers.IGetHandler;

public interface ITaskHandlerGet
{
    Task<PageResponse<Todo>> GetTaskListAsync(TodoDTO request);
    Task<Responses<Todo?>> GetByIdAsync(TodoDTO request);
}