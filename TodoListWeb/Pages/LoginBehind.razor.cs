using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using TodoList.Proj.Models;
using TodoListCore.ControllersHandlers;
using View.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly;
using Microsoft.JSInterop;
using MT.Blazor.ProtectedStorage;
using SecureIdentity.Password;
using TodoListCore.Interfaces;
using TodoListWeb.Handlers;


namespace TodoListWeb.Pages;

public  partial class LoginBehind : ComponentBase
{
   
   [Inject] private IJSRuntime? Js { get; set; }
   [Inject] public ISnackbar Snackbar { get; set; } = null;

   [Inject] public NavigationManager NavigationManager { get; set; } = null;
   
    public LoginDTO Inputmodel { get; set; } = new();
    public bool IsBusy { get; set; }

    
   [Inject] public ILoginHandler? Handler { get; set; } = null;


   public async Task OnvalidaSubmitasync()
   {
      

      try
      {
        
            
            var result = await Handler.LoginAsync(Inputmodel);
            if( result.Message.Contains("senha incorreta"))
            {
               Snackbar.Add(result.Message="senha incorreta", Severity.Error);
            }
            else
            {
              
                  await Js.InvokeVoidAsync("localStorage.setItem", "authToken", Handler.data );
                  Snackbar.Add(result.Message,Severity.Success);
                  await Task.Delay(200);
                  NavigationManager.NavigateTo("/");
               
            }
         
      }
      catch (Exception e)
      {
         Snackbar.Add(e.Message,Severity.Error);
      }
   }
   
}
