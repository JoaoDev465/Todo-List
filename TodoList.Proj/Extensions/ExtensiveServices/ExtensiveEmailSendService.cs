using TodoList.Proj.Services.EmailService;
using TodoListCore.Interfaces.InterfacesEmailServices;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveEmailSendService
{
    public static void EmailService(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IGenerateEmailService,GenerateEmailService>();
    }

}