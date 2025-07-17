using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TodoList.Proj.Extensions.ExtensiveObjects;

public  static  class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary dictionary)
    {
        var result = new List<string>();

        foreach (var itens in dictionary.Values)
        {
            result.AddRange(itens.Errors.Select(error => error.ErrorMessage));
        }

        return result;
    }
}