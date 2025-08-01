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
    private readonly IGenerateTokenService _service;

    public LoginWebHandler( HttpClient client,
        IGenerateTokenService service)
    {
        _client = client;
        _service = service;
    }
    public async Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request)
    {
        var token = _service.TokenGenerator(new User());
        var userlogin =  await _client.PostAsJsonAsync("api/v1/login", request);
        

        return userlogin.IsSuccessStatusCode
            ? new Responses<TokenResponse?>(new TokenResponse
            {
                Token = token
            })
            : new Responses<TokenResponse?>(null,400,"erro");
    }
    
}