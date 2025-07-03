using System.Data;
using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.UpdateControllers;


[ApiController]
[Route("api/v1/user/{id:int}")]

public class UpdateUserController : IUserPost
{
   private readonly Context _context;
   public UpdateUserController(Context context)
   {
      _context = context;
   }
   
  
   [HttpPut]
   public async Task<Responses<User?>> Updateasync(UserDto request)
   {
     
      var user = await _context.
         Users.FirstOrDefaultAsync(x => x.Id == request.Id);

      if (user == null)
      {
         return new Responses<User?>(null, 404, "Usuário Não Encontrado");
      }

      user.Name = request.UserName;
      user.Email = request.UserEmail;
      user.PasswordHash = request.UserPassword;

      try
      {
         _context.Update(user);
         await _context.SaveChangesAsync();
      }
      catch (Exception e)
      {
         return new Responses<User?>(null, 500, "Falha Interna No Servidor");
      }

      return new Responses<User?>(user, 200, "Usuário Atualizado com sucesso");
   }
}