using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoList.Proj;

namespace ApiKeyatributte.Usage;

[AttributeUsage(validOn: AttributeTargets.Class|AttributeTargets.Method)]
public class AttributeKey: Attribute,IAsyncActionFilter
{
    private const string APiHeaderKey = "x-api-key";
    private readonly string? _apikey;


    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var request = context.HttpContext.Request;
        if (!request.Headers.TryGetValue(APiHeaderKey, out var extracedkey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "api key não encontrada"
            };
            return;
        }

        if (string.Equals(extracedkey, _apikey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "api key inválida"
            };
            return;
        }

        await next();
    }
    
}
