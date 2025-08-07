using Microsoft.AspNetCore.Identity;
using SecureIdentity.Password;
using TodoList.Proj.Handlers.AuthHandlers;
using TodoList.Proj.Handlers.DeleteHandler;
using TodoList.Proj.Handlers.GetHandler;
using TodoList.Proj.Handlers.PostHandler;
using TodoList.Proj.Handlers.PutHandlers;
using TodoList.Proj.Models;
using TodoList.Proj.Services.TokenService;
using TodoListCore.ControllersHandlers;
using TodoListCore.IHandlers;
using TodoListCore.IHandlers.IDeleteHandlers;
using TodoListCore.IHandlers.IGetHandler;
using TodoListCore.IHandlers.IPutHandler;
using TodoListCore.Interfaces;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveHandlers
{
    public static void HandlerTaskDependencies(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddTransient<IPutTaskHandler,PutTaskHandler>();
        builder.Services.AddTransient<ITaskHandlerCreate,TaskhandlerCreate>();
        builder.Services.AddTransient<IDeleteTasksHandler, DeleteTaskHandler>();
        builder.Services.AddTransient<ITaskHandlerGet, TaskHandlerGet>();
        builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void HandlerUserDependencie(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
        builder.Services.AddTransient<IPutUserHandler,PutUserHandler>();
    }

    public static void HAndlerAuthLoginService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGenerateTokenService, GenerateTokenService>();
        builder.Services.AddTransient<ILoginHandler,LoginHandler>();
        builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    public static void HAndlerAuthRegisterService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
        builder.Services.AddTransient<IRegisterHandler,RegisterHandler>();
    }
}