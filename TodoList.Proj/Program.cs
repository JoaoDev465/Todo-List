using Apicontext.File;

var builder = WebApplication.CreateBuilder(args);
Configure(builder);
Services(builder);

// the configure method work for configure smtp,string connections and others
void Configure(WebApplicationBuilder builder)
{
    builder.Configuration.GetConnectionString("connection");
}
var app = builder.Build();

void Services(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<Context>();
}

app.Run();
