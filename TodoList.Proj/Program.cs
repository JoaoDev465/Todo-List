using TodoList.Proj.Extensions.ExtensiveAppConfigurations;
using TodoList.Proj.Extensions.ExtensiveServices;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddHttpContextAccessor();
builder.Configuration.AddEnvironmentVariables();

var dbConnection = Environment.GetEnvironmentVariable("Db_Connection")
                   ?? builder.Configuration.GetConnectionString("connection");

Console.WriteLine($"[Init] Database connection string: {(string.IsNullOrEmpty(dbConnection) ? "MISSING" : "FOUND")}");

try
{
    Console.WriteLine("[Init] Configurando serviços de handlers e dependências...");
    builder.HandlerTaskDependencies();
    builder.HandlerUserDependencie();
    builder.HAndlerAuthLoginService();
    builder.HAndlerAuthRegisterService();

    Console.WriteLine("[Init] Configurando DbContext...");
    builder.DbContextServices();

    Console.WriteLine("[Init] Configurando serviços de token, email e performance...");
    builder.TokenService();
    builder.EmailService();
    builder.PerformaceServices();

    Console.WriteLine("[Init] Configurando Swagger e controllers...");
    builder.SwaggerApplicationService();
    builder.ControllerServicesAndBehavior();
    builder.TokenServiceConfiguration();

    Console.WriteLine("[Init] Serviços configurados com sucesso.");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Falha ao configurar serviços: {ex}");
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
    Console.WriteLine($"[Init] Kestrel configurado na porta {port}");
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[FATAL] Unhandled exception: {ex}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Erro interno no servidor.");
    }
});

// 🔧 Aplica migrações sem derrubar a aplicação se falhar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Context>();
    try
    {
        Console.WriteLine("[Init] Aplicando migrações...");
        db.Database.Migrate();
        Console.WriteLine("[Init] Migrações aplicadas com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[WARN] Falha ao aplicar migrações: {ex.Message}");
    }
}

try
{
    Console.WriteLine("[Init] Configurando middleware e endpoints...");
    app.UseRouting();
    app.AuthenticantionAndAuthorization();
    app.SmtpConfigurationsGetvalues();
    app.ConfigurationsJsonsApiKey();
    app.UseSwagger();
    app.UseSwaggerUI();


    app.MapGet("/health", () => Results.Ok("Healthy"));

    app.MapControllers();

    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Serviços configurados com sucesso.");

    Console.WriteLine("[Init] Middleware e endpoints configurados com sucesso.");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Falha ao configurar middleware/endpoints: {ex}");
}

try
{
    Console.WriteLine("[Init] Iniciando aplicação...");
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Falha ao iniciar aplicação: {ex}");
}

namespace TodoList.Proj
{
    public partial class Program {}
}
