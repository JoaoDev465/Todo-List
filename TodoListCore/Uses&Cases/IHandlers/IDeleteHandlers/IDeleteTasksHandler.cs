using TodoList.Proj.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.IHandlers.IDeleteHandlers;

public interface IDeleteTasksHandler
{
    Task<Responses<Todo?>> DeleteAsync(TodoDto request);
}