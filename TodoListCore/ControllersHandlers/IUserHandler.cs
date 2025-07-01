using TodoList.Proj.Models;
using TodoListCore.Response;

namespace TodoListCore.ControllersHandlers;

public interface IUserHandler
{
    Task<Responses<User?>> Createasync(Use)
}