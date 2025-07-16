using TodoList.Proj.Models;
using TodoListCore.ControllersHandlers;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoList.Proj.Handlers.PostHandler;

public class TaskhandlerCreate: ITaskHandlerCreate
{
    public Task<Responses<Todo?>> CreateAsync(TodoDTO request)
    {
        throw new NotImplementedException();
    }
}