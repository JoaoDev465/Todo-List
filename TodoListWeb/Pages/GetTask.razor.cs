using System.Diagnostics.Contracts;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TodoListCore;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IGetHandler;
using TodoListWeb.Handlers.DeleteHAndler;

namespace TodoListWeb.Pages;

public partial class GetTask : ComponentBase
{
    [Inject] private ISnackbar Snackbar { get; set; } = null;
    [Inject] public ITaskHandlerGet TaskHandlerGet { get; set; } = null;
    [Inject] private IDialogService _dialogService { get; set; } = null;
    [Inject] public HttpClient Client { get; set; } = null;
    public TodoDto request { get; set; } = new();
    public List<Todo?> Tasks { get; set; } = [];
    public bool IsBusy { get; set; }
    public string SearchItem { get; set; } =String.Empty;
    protected override async Task OnInitializedAsync()
    {
        try
        {

            var request = new GetAllDatasDto();
            var response = await TaskHandlerGet.GetTaskListAsync(request);
            Console.Write(response);
            if (response.IsSuccess)
            {
                Tasks = response.Data ?? [];
                Console.Write(response);
            }
            else
            {
                Snackbar.Add("as tarefas estão nulas", Severity.Error);
                Console.Write(response);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async Task OnClkickDeleteButton(TodoDto request)
    {
        var result = await _dialogService.ShowMessageBox(
            "ATENÇÃO",
            $"ao proseguir a tarefa {request.Task} será concluída,portanto deletada,deseja continuar?",
           yesText:"CONCLUIR",
            cancelText:"CANCELAR");
        if (result is true) 
            await OnDeleteAsync(request);
        
        StateHasChanged();
    }

    public async Task OnDeleteAsync(TodoDto request)
    {
        try
        {
            var response =await  new DeleteHandler(Client).DeleteAsync(request);
            Tasks.RemoveAll(x => x.Id == request.Id);
            if (response.IsSuccess)
            {
                Snackbar.Add($"a tasks {request.Task} foi concluída", Severity.Success);
            }
            else
            {
                Snackbar.Add("Falha ao concluir a tarefa", Severity.Error);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Func<Todo, bool> Filter => todo =>
    {
        if (string.IsNullOrEmpty(SearchItem))
            return true;
        if (todo.Id.ToString().Contains(SearchItem, StringComparison.OrdinalIgnoreCase))
            return true;
        if (todo.Task.Contains(SearchItem, StringComparison.OrdinalIgnoreCase))
            return true;
        if (todo.Description is not null &&
            todo.Description.Contains(SearchItem, StringComparison.OrdinalIgnoreCase))
            return true;
        
        return false;
    };

}