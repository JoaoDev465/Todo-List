namespace TodoList.Proj.Extensions.ExtensiveAppConfigurations;

public static  class ExtensiveSmtpConfigurations
{
    public static void SmtpConfigurationsGetvalues(this WebApplication builder)
    {
        var smtp = new SmTpService();
        builder.Configuration.GetSection("ConfSMTP").Bind(smtp);
        Configuration.SmTpService = smtp;
        if (string.IsNullOrEmpty(smtp.ToString()))
        {
            Console.WriteLine("falha ao gerar o smtp, está nulo");
        }
    }
}