using TodoList.Proj.Models;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoListCore.IHandlers.IPutHandler;

public interface IPutTaskHandler
{
    Task<Responses<Todo?>> PutAsync(TodoDTO request);
}