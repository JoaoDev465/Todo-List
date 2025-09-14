using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveDbcontextService
{
   public static void DbContextServices(this WebApplicationBuilder builder)
   {
      var dbContextConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__connection")
                                      ?? builder.Configuration.GetConnectionString("connection");
      builder.Services.AddDbContext<Context>(x => x.UseSqlServer(dbContextConnectionString,
          optionsBuilder =>
          {
              optionsBuilder.CommandTimeout(60);
              
              optionsBuilder.EnableRetryOnFailure(
                  maxRetryCount: 5,
                  maxRetryDelay: TimeSpan.FromSeconds(10),
                  errorNumbersToAdd: null);
          } ));
  
   }

}