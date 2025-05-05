using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoList.Proj;

namespace ApiKeyatributte.Usage;

[AttributeUsage(validOn: AttributeTargets.Class|AttributeTargets.Method)]
public class AttributeKey: Attribute,IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Query.TryGetValue(
                Configuration.ApiKey, out var extracedkey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "api key não encontrada"
            };
            return;
        }

        if (!Configuration.ApiKey.Equals(extracedkey))
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
