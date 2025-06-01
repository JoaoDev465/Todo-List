using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;

namespace TodoList.Proj.InterfaceModel;

public class TokenModel
{
    public string user;
}

public interface IGenerateTokenService
{
    public string returnToken(TokenModel model);
}