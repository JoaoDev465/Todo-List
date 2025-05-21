using Apicontext.File;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Proj.ExtensionMethods;

public static class ExtensiveDbcontextService
{
   public static void DbContextServices(this WebApplicationBuilder builder)
   {
      var DbContextConnectionString =  builder.Configuration.GetConnectionString("connection");
      builder.Services.AddDbContext<Context>(x => x.UseSqlServer(DbContextConnectionString));
  
   }

}