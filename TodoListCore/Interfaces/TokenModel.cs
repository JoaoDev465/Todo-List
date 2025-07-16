
using TodoList.Proj.Models;

namespace TodoListCore.Interfaces;


public interface IGenerateTokenService
{
    string TokenGenerator(User user);
}