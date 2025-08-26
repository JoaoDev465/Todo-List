using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Handlers;

namespace TodoListWeb.Pages;

public partial class Behind : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null;
    [Inject] public ISnackbar Snackbar { get; set; } = null;
    [Inject] public NavigationManager NavigationManager { get; set; } = null;
    [Inject] public ILoginHandler Handler { get; set; } = null;
    public LoginDTO InputModel { get; set; } = new();
    public bool Isbusy { get; set; } = false;

    public async Task OnValidSubmitAsync()
    {
        Isbusy = true;
        try
        {
            var result = await Handler.LoginAsync(InputModel);
            if (result.IsError)
            {
                Snackbar.Add(result.Message = "senha inválida", Severity.Error);
            }
            else
            {
                await JsRuntime.InvokeVoidAsync("localStorage.setItem", "AuthToken", result.Data);
                Snackbar.Add(result.Message, Severity.Success);
                await Task.Delay(200);
                NavigationManager.NavigateTo("/");
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            Isbusy = false;
        }
    }
}