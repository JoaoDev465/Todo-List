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
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;

var builder = WebApplication.CreateBuilder(args); 
DbContextServices(builder);
ControllerServicesAndBehavior(builder);
TokenService(builder);
TokenConfiguration(builder);
EmailService(builder);
SwaggerAplicationService(builder);
PerformaceServices(builder);

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

void SwaggerAplicationService(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

void PerformaceServices(WebApplicationBuilder builder)
{
    builder.Services.AddMemoryCache();
    builder.Services.AddResponseCompression
    (options =>
        options.Providers.Add<GzipCompressionProvider>()
    );
    builder.Services.Configure<GzipCompressionProviderOptions>(
        x =>
        {
            x.Level = CompressionLevel.Optimal;
        });
}

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("este é o ambiente de desenvovimento");
}
   

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
    app.Configuration.GetSection("ConfSMTP").Bind(smtp);
    Configuration._SmTpService = smtp;
    if (string.IsNullOrEmpty(smtp.ToString()))
    {
        Console.WriteLine("falha ao gerar o smtp, está nulo");
    }
    
}

