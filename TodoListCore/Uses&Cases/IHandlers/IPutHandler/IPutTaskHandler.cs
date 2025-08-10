using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers.IPutHandler;

public interface IPutTaskHandler
{
    Task<Responses<Todo?>> PutAsync(TodoDto request);
}