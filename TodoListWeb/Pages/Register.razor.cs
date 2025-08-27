using Microsoft.AspNetCore.Components;
using MudBlazor;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Handlers;
using TodoListWeb.Security;

namespace TodoListWeb.Pages;

public partial class Registrador : ComponentBase
{
    [Inject] private JwtSecurityProvider JwtSecurityProvider { get; set; } = null;
    [Inject]   public NavigationManager TypeNavigationManager { get; set; } = null;
    [Inject]  public ISnackbar Snackbar { get; set; } = null;
    public UserDto InputModel { get; set; } = new();
    public bool Isbusy { get; set; } = false;
    [Inject] public IRegisterHandler Handler { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        var authstate = await JwtSecurityProvider.GetAuthenticationStateAsync();
        var user = authstate.User;
        if (user.Identity is { IsAuthenticated: true })
        {
            TypeNavigationManager.NavigateTo("/login");
        }
    }

    public async Task OnValidSubmitAsync()
    {
        Console.WriteLine(">>> Disparou Register");

        Isbusy = true;
        try
        {
            var result = await Handler.RegisterAsync(InputModel);
            if (result.Code == 201)
            {
                Snackbar.Add(result.Message, Severity.Success);
                TypeNavigationManager.NavigateTo("/login");
                await Task.Delay(200);
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
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