using System.Net.Http.Json;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IGetHandler;

namespace TodoListWeb.Handlers.GetHandler;

public class GetHandler: ITaskHandlerGet
{
    private readonly HttpClient _client;

    public GetHandler(HttpClient client)
    {
        _client = client;
    }

    public async Task<PageResponse<List<Todo>>> GetTaskListAsync(GetAllDatasDto request)
    {
        var response = await _client.GetFromJsonAsync<PageResponse<List<Todo>>>
            ("api/v1/tasks");
        Console.Write(response);
        if (response != null && response.Code == 200)
        {
          return  new PageResponse<List<Todo>>(response.Data,response.TotalCount,response.Code,response.CurrentCount,response.PageSize);
        }
        else
        {
            return new PageResponse<List<Todo>>(null, 400, "Erro ao gerar a lista");
        }

    }
    

    public async Task<Responses<Todo?>> GetByIdAsync(int id)
    {
        var response = await _client.GetFromJsonAsync<Todo>($"api/v1/task/{id:int}");
        try
        {
            if (response != null)
            {
                return new Responses<Todo?>(response, 200, null);
            }

            return new Responses<Todo?>(null, 400, "falha ao retornar a tarefa");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}