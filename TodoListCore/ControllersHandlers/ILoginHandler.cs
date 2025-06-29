using TodoListCore.Requests;
using TodoListCore.Response;

namespace TodoListCore.ControllersHandlers;

public interface ILoginHandler
{
   public Task<Responses<string?>> LoginRequest(LoginRequest request);
}