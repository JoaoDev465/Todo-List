using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers.IDeleteHandlers;

public interface IDeleteTasksHandler
{
    Task<Responses<Todo?>> DeleteAsync(TodoDto request);
}