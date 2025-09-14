using TodoListCore.Response;

namespace TodoListCore.Uses_Cases.IHandlers;

public interface ILogoutHandler
{
    public Task LogoutAsync();
}