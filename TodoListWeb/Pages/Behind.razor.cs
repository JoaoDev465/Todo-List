using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Handlers;
using TodoListWeb.Security;

namespace TodoListWeb.Pages;

public partial class Behind : ComponentBase
{
    [Inject] private JwtSecurityProvider _jwtSecurityProvider { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; } = null;
    [Inject] public NavigationManager NavigationManager { get; set; } = null;
    [Inject] public ILoginHandler Handler { get; set; } = null;
    public LoginDTO InputModel { get; set; } = new();
    public bool Isbusy { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        var authstate = await _jwtSecurityProvider.GetAuthenticationStateAsync();
        var user = authstate.User;
        
        if(user.Identity is {IsAuthenticated: true})
            NavigationManager.NavigateTo("/home");
    }

    public async Task OnValidSubmitAsync()
    {
        Isbusy = true;
        try
        {
            var result = await Handler.LoginAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message = "Login Feito Com Sucesso", Severity.Success);
                _jwtSecurityProvider.NotifyAuthenticationStateChanged();
                await Task.Delay(200);
                NavigationManager.NavigateTo("/home");
            }
            else
            {
                Snackbar.Add(result.Message = "senha inválida", Severity.Error);
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