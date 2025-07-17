using TodoList.Proj.InterfaceModel.InterfacesEmailServices;
using TodoList.Proj.Services.EmailService;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveEmailSendService
{
    public static void EmailService(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IGenerateEmailService,GenerateEmailService>();
    }

}