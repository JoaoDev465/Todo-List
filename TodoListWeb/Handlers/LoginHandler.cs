using System.Net.Http.Json;
using System.Text.Json;
using TodoListCore.Interfaces;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;

namespace TodoListWeb.Handlers;

public class LoginHandler : ILoginHandler
{
    
    private readonly HttpClient _client;

    public LoginHandler( HttpClient client)
    {
       
        _client = client;
    }

    public async Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("api/v1/login", request);
            if (!response.IsSuccessStatusCode)
                return new Responses<TokenResponse?>(null, 400, "Bad Request");

            var tokenresponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return new Responses<TokenResponse?>(tokenresponse,200,null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}
    