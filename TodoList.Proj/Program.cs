using System.Text;
using Apicontext.File;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj;
using TodoList.Proj.TokenGenerator;

var builder = WebApplication.CreateBuilder(args); 
DbContextServices(builder);
ControllerServicesAndBehavior(builder);
TokenService(builder);
TokenConfiguration(builder);


void DbContextServices(WebApplicationBuilder builder)
{
    var DbContextConnectionString =  builder.Configuration.GetConnectionString("connection");
      builder.Services.AddDbContext<Context>(x => x.UseSqlServer(DbContextConnectionString));
  
}

void ControllerServicesAndBehavior(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(
        x => { x.SuppressModelStateInvalidFilter = true;});

}

void TokenService(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<TokenService>();
}

void TokenConfiguration(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
        x => x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true
        });
}


var app = builder.Build();
SettingsToStartApplicationAndItsServices(app);



void SettingsToStartApplicationAndItsServices(WebApplication app)
{
    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization(); 
    app.Run();
}

