using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers.IPutHandler;

public interface IPutUserHandler
{
    Task<Responses<User?>> PutAsync(UserDto request);
}