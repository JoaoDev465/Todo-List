using System.Text;
using Apicontext.File;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj;
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;

var builder = WebApplication.CreateBuilder(args); 
DbContextServices(builder);
ControllerServicesAndBehavior(builder);
TokenService(builder);
TokenConfiguration(builder);
EmailService(builder);

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
    builder.Services.AddSingleton<GenerateTokenService>();
}

void EmailService(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<GenerateEmailService>();
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



AuthenticantionAndAuthorization(app);
ConfigurationsJSONSmtp_Jwt_ApiKey(app);


app.MapControllers();
app.Run();

void AuthenticantionAndAuthorization(WebApplication builder)
{
    app.UseAuthentication();
    app.UseAuthorization(); 
}


void ConfigurationsJSONSmtp_Jwt_ApiKey(WebApplication builder)
{
    app.Configuration.GetValue<string>("apikey");
    var smtp = new SmTpService();
    app.Configuration.GetSection("ConfigurationSMTP").Bind(smtp);
}
