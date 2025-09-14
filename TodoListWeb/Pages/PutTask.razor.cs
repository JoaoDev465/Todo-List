using Microsoft.AspNetCore.Components;
using MudBlazor;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListCore.Uses_Cases.IHandlers.IGetHandler;
using TodoListCore.Uses_Cases.IHandlers.IPutHandler;
using TodoListWeb.Handlers.GetHandler;

namespace TodoListWeb.Pages;

public partial class PutTask : ComponentBase
{
    [Inject] public ITaskHandlerGet _taskHandlerGet { get; set; } = null;
    [Parameter] public int Id { get; set; }
    [Inject] private ISnackbar _snackbar { get; set; } = null;
    [Inject] public IPutTaskHandler Handler { get; set; } = null;
    [Inject] public NavigationManager NavigationManager { get; set; } = null;
    public TodoDto Request { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (Id != 0)
            {
                var response = await _taskHandlerGet.GetByIdAsync(Id);
                if (response.IsSuccess)
                {
                    Request = new TodoDto
                    {
                        Id = response.Data.Id,
                        DescriptionOfTask = response.Data.Description,
                        Task = response.Data.Task
                    };
                }
                else
                {
                    _snackbar.Add("Falha interna no servidor", Severity.Error);
                }
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
  

    protected async Task PuTAsync()
    {
        var response = await Handler.PutAsync(Id,Request);
        try
        {
            if (response.IsSuccess)
            {
               
                _snackbar.Add("Tarefa atualizada com sucesso", Severity.Success);
                NavigationManager.NavigateTo("/ver/tarefas", forceLoad:true);

                
            }
            else
            {
                _snackbar.Add("falha ao atualizar a tarefa", Severity.Error);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}