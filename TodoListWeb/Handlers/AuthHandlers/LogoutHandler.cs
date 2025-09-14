using TodoListCore.Uses_Cases.IHandlers;
using TodoListWeb.Security;

namespace TodoListWeb.Handlers.AuthHandlers;

public class LogoutHandler: ILogoutHandler
{
    private readonly JwtSecurityProvider _provider;
    

    public LogoutHandler(JwtSecurityProvider provider)
    {
        _provider = provider;
    }

    public async Task LogoutAsync()
    {
        await _provider.MarkUserAuthLoggedOut();
    }
}