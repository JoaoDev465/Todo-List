using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace TodoListFront.Pages;

public partial class LoginBehind : ComponentBase
{
    [Inject] public ISnackbar Snackbar { get; set; } = null;
    
   
}