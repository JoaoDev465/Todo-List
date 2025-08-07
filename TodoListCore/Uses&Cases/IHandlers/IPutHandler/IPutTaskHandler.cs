using TodoList.Proj.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.IHandlers.IPutHandler;

public interface IPutTaskHandler
{
    Task<Responses<Todo?>> PutAsync(TodoDto request);
}