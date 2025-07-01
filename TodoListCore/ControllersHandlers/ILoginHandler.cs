
using TodoList.Proj.Models;
using TodoListCore.Response;
using View.ViewModels;

namespace TodoListCore.ControllersHandlers;

public interface ILoginHandler
{
   public Task<Responses<User?>> LoginRequest(LoginDTO request);
}