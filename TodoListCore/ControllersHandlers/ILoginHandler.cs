
using TodoList.Proj.Models;
using TodoListCore.Response;
using View.ViewModels;
using ViewModels.User;

namespace TodoListCore.ControllersHandlers;

public interface ILoginHandler
{
   public Task<Responses<User?>> LoginAsync(LoginDTO request);
   public Task<Responses<User?>> RegisterAsync(UserDto request);
   
}