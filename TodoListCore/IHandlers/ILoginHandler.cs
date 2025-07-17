using TodoListCore.Response;
using View.ViewModels;

namespace TodoListCore.IHandlers;

public interface ILoginHandler
{
   
   public Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request);
 
   
}