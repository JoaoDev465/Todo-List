using Microsoft.AspNetCore.Components;
using MudBlazor;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IGetHandler;

namespace TodoListWeb.Pages;

public partial class GetTask : ComponentBase
{
    [Inject] private ISnackbar Snackbar { get; set; } = null;
    [Inject] public ITaskHandlerGet TaskHandlerGet { get; set; } = null;
    public TodoDto WriteModel { get; set; } = new();
    public List<TodoDto> Tasks { get; set; } = new();
    
}