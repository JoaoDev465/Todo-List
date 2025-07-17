using System.Net;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Program = TodoList.Proj.Program;

namespace TodoListTest.TestExtensiveServices.TesteExtensioServicesSwagger;

public class TestSwagger:IClassFixture<WebApplicationFactory<Program>>
{
   private readonly HttpClient _httpClient;

   public TestSwagger(WebApplicationFactory<Program> factory)
   {
      _httpClient = factory.CreateClient();
   }

   [Fact]
   public async Task SwaggerTest_Returns_Ok()
   {
      var response = await _httpClient.GetAsync("http://localhost:5280/swagger");

      Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var content = await response.Content.ReadAsStringAsync();
      Xunit.Assert.Contains("Swagger UI", content);
   }
}

