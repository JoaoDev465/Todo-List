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
            c.JsonSerializerOptions.ReferenceHandler = null;
            c.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
    }

}