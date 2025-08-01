using TodoList.Proj.Atributtes.ApiKeyAtributte;
using TodoList.Proj.Extensions.ExtensiveAppConfigurations;
using TodoList.Proj.Extensions.ExtensiveServices;

var builder = WebApplication.CreateBuilder(args); 
builder.CorsServices();
builder.HandlerTaskDependencies();
builder.HandlerUserDependencie();
builder.HAndlerAuthLoginService();
builder.HAndlerAuthRegisterService();
builder.DbContextServices();
builder.TokenService();
builder.EmailService();
builder.PerformaceServices();
builder.SwaggerApplicationService();
builder.ControllerServicesAndBehavior();
builder.TokenServiceConfiguration();
builder.TestAuthenticationEscheme();
builder.TestAuthenticationSchemeAddChallengeAuth();
var app = builder.Build();

string? ConnectionCors = builder.Configuration.GetValue<string>("BeckEndCorsName");
app.UseCors("allow");
app.AuthenticantionAndAuthorization();
app.SmtpConfigurationsGetvalues();
app.ConfigurationsJSONSApiKey();
app.MapControllers();


if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("este é o ambiente de desenvovimento");
}

app.Run();



namespace TodoList.Proj
{
    public partial class Program {}
}

    
    


