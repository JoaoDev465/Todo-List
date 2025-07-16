
using TodoList.Proj.Models;
using TodoListCore.DTO;
using TodoListCore.Response;
using View.ViewModels;
using ViewModels.User;

namespace TodoListCore.ControllersHandlers;

public interface ILoginHandler
{
   
   public Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request);
 
   
}