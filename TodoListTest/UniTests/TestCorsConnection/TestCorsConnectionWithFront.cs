using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace TodoListTest.UniTests.TestCorsConnection;

public class TestCorsConnectionWithFront
{
    [Fact]
    public void TestConnection()
    {
        var config = new ConfigurationBuilder().
            AddJsonFile("appsettings.Development.tests.json").Build();
        var connection = config.GetValue<string>("FrontUri:Secret");

        var services = new ServiceCollection();

        services.AddCors(x =>
        {
            x.AddPolicy("allow", options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.WithOrigins(connection);
            });
        });

        var provider = services.BuildServiceProvider();
        var corsOption = provider.GetRequiredService<IOptions<CorsOptions>>().Value;
        var corsValue = corsOption.GetPolicy("allow");
        Xunit.Assert.NotNull(connection);
        Xunit.Assert.Contains(connection,corsValue.Origins);
    }
}