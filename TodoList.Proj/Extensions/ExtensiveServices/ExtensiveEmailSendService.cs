using TodoList.Proj.InterfaceModel;
using TodoList.Proj.Services.EmailService;

namespace TodoList.Proj.ExtensionMethods;

public static class ExtensiveEmailSendService
{
    public static void EmailService(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IGenerateEmailService,GenerateEmailService>();
    }

}