using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;

namespace TodoListCore.Uses_Cases.IHandlers;

public interface ILoginHandler
{
   
   public Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request);
 
   
}