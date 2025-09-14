using System.Text.Json;
using System.Text.Json.Serialization;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveControllerServices
{
    public static void ControllerServicesAndBehavior(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().ConfigureApiBehaviorOptions(x =>
        {
            x.SuppressModelStateInvalidFilter = true;
        }).AddJsonOptions(c =>
        {
            c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            c.JsonSerializerOptions.WriteIndented = true;
            c.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            c.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
    }

}