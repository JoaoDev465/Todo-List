
using TodoList.Proj.Extensions.ExtensiveAppConfigurations;
using TodoList.Proj.Extensions.ExtensiveServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
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
var app = builder.Build();


app.UseCors("allow");
app.UseRouting();
app.AuthenticantionAndAuthorization();
app.SmtpConfigurationsGetvalues();
app.ConfigurationsJsonsApiKey();
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

    
    


