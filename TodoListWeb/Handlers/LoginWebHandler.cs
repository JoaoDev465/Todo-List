using System.Net.Http.Json;
using TodoList.Proj.Models;
using TodoListCore.ControllersHandlers;
using TodoListCore.IHandlers;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using TodoListWeb.Pages;
using View.ViewModels;
using ViewModels.User;

namespace TodoListWeb.Handlers;

public class LoginWebHandler : ILoginHandler
{
    private ILoginHandler _loginHandlerImplementation;
    private readonly HttpClient _client;


    public int Code { get; set; }
    public string Message{ get; set; }
    public string data { get; set; }
  

    public LoginWebHandler( HttpClient client)
    {
        _client = client;
    }
    public async Task <Responses<string>> LoginAsync(LoginDTO request)
    {
        var userlogin =  await _client.PostAsJsonAsync("api/v1/login", request);
        

        return userlogin.IsSuccessStatusCode
            ? new Responses<string>("login realizado com sucesso", 200, "login realizado com sucesso")
            : new Responses<string>(null,400,"erro");
    }

    public Task<Responses<User?>> RegisterAsync(UserDto request)
    {
        return _loginHandlerImplementation.RegisterAsync(request);
    }
}