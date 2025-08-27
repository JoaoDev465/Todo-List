using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using TodoListCore.Interfaces;
using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb;
using TodoListWeb.Handlers;
using TodoListWeb.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<JwtSecurityProvider>();
builder.Services.AddScoped<ILoginHandler, LoginHandler>();
builder.Services.AddScoped<IRegisterHandler, RegisterHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5280/") });

await builder.Build().RunAsync();
