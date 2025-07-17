using TodoList.Proj.Models;
using TodoListCore.Response;
using ViewModels.User;

namespace TodoListCore.IHandlers.IPutHandler;

public interface IPutUserHandler
{
    Task<Responses<User?>> PutAsync(UserDto request);
}