namespace TodoList.Proj.ExtensionMethods;

public static class ExtensiveControllerServices
{
    public static void ControllerServicesAndBehavior(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().ConfigureApiBehaviorOptions(
            x => { x.SuppressModelStateInvalidFilter = true;});

    }

}