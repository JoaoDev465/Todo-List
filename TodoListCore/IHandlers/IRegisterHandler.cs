using TodoList.Proj.Models;
using TodoListCore.Response;
using ViewModels.User;

namespace TodoListCore.Interfaces;

public interface  IRegisterHandler
{
    public Task<Responses<User>> RegisterAsync(UserDto request);
}