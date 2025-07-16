using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;
using TodoListCore.ControllersHandlers;
using TodoListCore.Interfaces;
using TodoListWeb;
using TodoListWeb.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5280/") });
builder.Services.AddTransient<ILoginHandler,LoginWebHandler>();

await builder.Build().RunAsync();
