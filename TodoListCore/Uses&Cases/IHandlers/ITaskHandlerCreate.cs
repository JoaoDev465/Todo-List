using TodoList.Proj.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.ControllersHandlers;

public interface ITaskHandlerCreate
{
    Task<Responses<Todo?>> CreateAsync(TodoDto request);
    
}