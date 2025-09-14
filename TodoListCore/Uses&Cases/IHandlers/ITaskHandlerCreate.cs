using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers;

public interface ITaskHandlerCreate
{
    Task<Responses<Todo?>> CreateAsync(TodoDto request);
    
}