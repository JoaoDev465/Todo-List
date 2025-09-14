using System.Net.Http.Json;
using System.Text.Json;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IDeleteHandlers;

namespace TodoListWeb.Handlers.DeleteHAndler;

public class DeleteHandler: IDeleteTasksHandler
{
    private readonly HttpClient _client;

    public DeleteHandler(HttpClient client)
    {
        _client = client;
    }
    public async Task<Responses<Todo?>> DeleteAsync(TodoDto request)
    {
        try
        {
            var response = await _client.DeleteAsync($"api/v1/delete/{request.Id}");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = await response.Content.ReadFromJsonAsync<Todo?>(options);
            if (response.IsSuccessStatusCode)
            {
                return new Responses<Todo?>(result, 200, "item deletado com sucesso");
            }
            else
            {
                return new Responses<Todo?>(null, 400, "falha na tentaiva de deletar");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}