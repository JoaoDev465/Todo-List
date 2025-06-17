using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Extensions.ExtensiveObjects;
using TodoList.Proj.Models;
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;

namespace TodoList.Proj.InterfaceModel;

public class TokenModel
{
    public string user;
}

public interface IGenerateTokenService
{
    public string TokenGenerator(User user);
}