using System.Net.Http.Json;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IPutHandler;

namespace TodoListWeb.Handlers.PutHandler;

public class PutTaskHandler: IPutTaskHandler
{
    private readonly HttpClient _client;

    public PutTaskHandler(HttpClient client)
    {
        _client = client;
    }
    public async Task<Responses<Todo?>> PutAsync(int id,TodoDto request)
    {
        var response = await _client.PutAsJsonAsync($"api/v1/task/{id}",request);
        try
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<Todo>();
                return new Responses<Todo?>(content, 200, "Tarefa atualizado com sucesso");
            }
            else
            {
                return new PageResponse<Todo?>(null, 400, "Falha ao atualizar a tarefa");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}