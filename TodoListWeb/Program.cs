using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using TodoListCore.Interfaces;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListCore.Uses_Cases.IHandlers.IDeleteHandlers;
using TodoListCore.Uses_Cases.IHandlers.IGetHandler;
using TodoListCore.Uses_Cases.IHandlers.IPutHandler;
using TodoListWeb;
using TodoListWeb.Handlers;
using TodoListWeb.Handlers.DeleteHAndler;
using TodoListWeb.Handlers.GetHandler;
using TodoListWeb.Handlers.PutHandler;
using TodoListWeb.Handlers.TaskHandler;
using TodoListWeb.Pages;
using TodoListWeb.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<ITaskHandlerGet, GetHandler>();
builder.Services.AddScoped<IPutTaskHandler, PutTaskHandler>();
builder.Services.AddScoped<IDeleteTasksHandler, DeleteHandler>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ITaskHandlerGet, GetHandler>();
builder.Services.AddScoped<ITaskHandlerCreate, CreateTaskHandler>();
builder.Services.AddScoped<JwtSecurityProvider>();
builder.Services.AddScoped<AuthenticationStateProvider,JwtSecurityProvider>();
builder.Services.AddScoped<ILoginHandler, LoginHandler>();
builder.Services.AddScoped<IRegisterHandler, RegisterHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5280/") });

await builder.Build().RunAsync();
