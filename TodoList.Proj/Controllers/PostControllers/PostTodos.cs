using System.Runtime.InteropServices.JavaScript;
using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoListCore.ControllersHandlers;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.PostControllers;

[ApiController]
[Route("api/v1/task")]

public class PostTodoController: ITaskPost
{
    
    private readonly Context _context;
   
    public PostTodoController(Context context)
    {
        _context = context;
    }
   
    [HttpPost]
    public async Task<Responses<Todo?>> Createasync(
         TodoDTO request)
    {
       
        var userTodo = new Todo
        {
            Start = request.Start_Task,
            Initialized = DateTime.Now,
            Task = request.Task,
            Description = request.DescriptionOfTask,
            Alert = DateTime.Now,
            Finalized = request.FinalizedTimeTask,
            UserId = request.userId
        };

        try
        {
            await _context.AddAsync(userTodo);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new Responses<Todo?>(null, 500, "Falha Interna no servidor");
        }

        return new Responses<Todo?>(userTodo, 201, "Tarefa Criada Com Sucesso");
    }
}