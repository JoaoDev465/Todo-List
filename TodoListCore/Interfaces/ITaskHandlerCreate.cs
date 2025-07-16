using TodoList.Proj.Models;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoListCore.ControllersHandlers;

public interface ITaskHandlerCreate
{
    Task<Responses<Todo?>> CreateAsync(TodoDTO request);
    
}