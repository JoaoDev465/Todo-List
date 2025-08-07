using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveDbcontextService
{
   public static void DbContextServices(this WebApplicationBuilder builder)
   {
      var dbContextConnectionString =  builder.Configuration.GetConnectionString("connection");
      builder.Services.AddDbContext<Context>(x => x.UseSqlServer(dbContextConnectionString));
  
   }

}