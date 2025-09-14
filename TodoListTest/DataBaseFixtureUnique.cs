using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;

namespace TodoListTest;

public class DataBaseFixtureUnique: IDisposable
{

     public  Context Dbcontext { get; private set; } = null!;

     public void DatabaseFixture()
     {
          var options = new DbContextOptionsBuilder<Context>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

          Dbcontext = new Context(options);

     }

     public void Dispose()
     {
         Dbcontext?.Dispose();
     }
}