using System.IO.Compression;
using System.Text;
using Apicontext.File;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Extensions.ExtensiveAppConfigurations;
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;

var builder = WebApplication.CreateBuilder(args); 
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



public partial class Program {}

    
    


