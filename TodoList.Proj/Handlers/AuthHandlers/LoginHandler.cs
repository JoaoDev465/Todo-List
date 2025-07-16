using TodoListCore.ControllersHandlers;
using TodoListCore.Response;
using View.ViewModels;

namespace TodoList.Proj.Controllers.PostControllers;

public class LoginHandler : ILoginHandler
{
    public int Code { get; set; }
    public string Message { get; set; }
    public string data { get; set; }
    public Task<Responses<string>> LoginAsync(LoginDTO request)
    {
        throw new NotImplementedException();
    }
}