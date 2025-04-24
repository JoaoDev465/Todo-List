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
Configure(builder);
Services(builder);
tokenConf(builder);

// the configure method work for configure smtp,string connections and others
void Configure(WebApplicationBuilder builder)
{
   
}

void Services(WebApplicationBuilder builder)
{
    var connectionString =  builder.Configuration.GetConnectionString("connection");
    builder.Services.AddOptions();
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(
        x => { x.SuppressModelStateInvalidFilter = true;});
    builder.Services.AddDbContext<Context>(x => x.UseSqlServer(connectionString));
    builder.Services.AddSingleton<TokenService>();
}

void tokenConf(WebApplicationBuilder builder)
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

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();