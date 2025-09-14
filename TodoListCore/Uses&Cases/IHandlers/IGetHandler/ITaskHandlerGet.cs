using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers.IGetHandler;

public interface ITaskHandlerGet
{
    Task<PageResponse<List<Todo?>>> GetTaskListAsync(GetAllDatasDto dto);
    Task<Responses<Todo?>> GetByIdAsync(int id);
}