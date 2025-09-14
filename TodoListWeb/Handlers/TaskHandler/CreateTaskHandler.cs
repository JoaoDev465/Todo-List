using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Security;

namespace TodoListWeb.Handlers.TaskHandler;

public class CreateTaskHandler: ITaskHandlerCreate
{
    private readonly HttpClient _client;
    private readonly JwtSecurityProvider _provider;
    public CreateTaskHandler(HttpClient client,JwtSecurityProvider provider)
    {
        _client = client;
        _provider = provider;
    }
    public async Task<Responses<Todo?>> CreateAsync(TodoDto request)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("api/v1/post", request);
            if (!response.IsSuccessStatusCode)
            {
                return new Responses<Todo?>(null,400,"Erro: Bad request");
            }
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = await response.Content.ReadFromJsonAsync<Todo>(options);

            return new Responses<Todo?>(result, 201, "Tarefa criada com sucesso");
        }
        catch (Exception e)
        {
            return new Responses<Todo?>(null, 500, e.Message);
        }
    }
}