
using TodoList.Proj.Models;
using TodoListCore.Response;
using View.ViewModels;
using ViewModels.User;

namespace TodoListCore.ControllersHandlers;

public interface ILoginHandler
{
   public int Code { get; set; }
   public string Message { get; set; }
   public  string data { get; set; }
   public Task<Responses<string>> LoginAsync(LoginDTO request);
 
   
}