using System.Net.Http.Json;
using System.Text.Json;
using TodoListCore.Interfaces;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Security;

namespace TodoListWeb.Handlers;

public class LoginHandler : ILoginHandler
{
    
    private readonly HttpClient _client;
    private readonly JwtSecurityProvider _provider;

    public LoginHandler( HttpClient client, JwtSecurityProvider provider)
    {
       
        _client = client;
        _provider = provider;
    }

    public async Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("api/v1/login", request);
            if (!response.IsSuccessStatusCode)
                return new Responses<TokenResponse?>(null, 400, "Bad Request");

            var tokenresponse = await response.Content.ReadFromJsonAsync<Responses<TokenResponse>>();

            await _provider.MarkUserIsAuth(tokenresponse.Data.Token);
            
            return new Responses<TokenResponse?>(tokenresponse.Data,200,null);
        }
        catch (Exception e)
        {
            return new Responses<TokenResponse?>(null, 500, e.Message);
        }

    }
}
    