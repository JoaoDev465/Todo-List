using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers;

public interface  IRegisterHandler
{
    public Task<Responses<User?>> RegisterAsync(UserDto request);
}