using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Pages;

namespace TodoListWeb.Handlers;

public class RegisterHandler: IRegisterHandler
{
    private readonly HttpClient _client;
    public RegisterHandler(HttpClient client)
    {
        _client = client;
    }
    public async Task<Responses<User?>> RegisterAsync(UserDto request)
    {
        try
        {
           
            var response = await _client.PostAsJsonAsync("api/v1/register",request );
            if (!response.IsSuccessStatusCode)
            {
                return new Responses<User?>(null, 400, "Bad request");
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = await response.Content.ReadFromJsonAsync<User>(options);
            
            return new Responses<User?>(result,201,"usuário Cadastrado com sucesso");
        }
        catch (Exception e)
        {
            return new Responses<User?>(null, 500, e.Message);
        }
    }
}