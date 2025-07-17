using TodoList.Proj.Models;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoListCore.IHandlers.IDeleteHandlers;

public interface IDeleteTasksHandler
{
    Task<Responses<Todo?>> DeleteAsync(TodoDTO request);
}