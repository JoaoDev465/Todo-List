using TodoList.Proj.Models;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoListCore.ControllersHandlers;

public interface ITaskHandler
{
    Task<Responses<Todo?>> CreateAsync(TodoDTO request);
    Task<Responses<Todo?>> PutAsync(TodoDTO request);
    Task<Responses<Todo?>> DeleteAsync(TodoDTO request);
    Task<Responses<Todo?>> GetteAsync(TodoDTO request);
    
    Task<PageResponse<List<Todo?>>> GetAlleAsync(TodoDTO request);
    
}