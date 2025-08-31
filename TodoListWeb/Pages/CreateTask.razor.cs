using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Security;

namespace TodoListWeb.Pages;

public partial class CreateTask : ComponentBase
{
    [Inject] public JwtSecurityProvider Provider { get; set; } = null;
    [Inject] private ISnackbar _snackbar { get; set; } = null;
    [Inject] public ITaskHandlerCreate HandlerCreate { get; set; } = null;
    public TodoDto InputModel { get; set; } = new();
    public bool Isbusy { get; set; }
    

    public async Task CreateAsync()
    {
        Isbusy = true;
        try
        {
            var response = await HandlerCreate.CreateAsync(InputModel);
            if (response.IsSuccess)
            {
               
                _snackbar.Add(response.Message, Severity.Success);
            }
            else
            {
                _snackbar.Add(response.Message??"Erro na criação de tarefa", Severity.Error);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Isbusy = false;
        }
    }
}