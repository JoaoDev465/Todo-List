using Microsoft.AspNetCore.Mvc.ModelBinding;
using ViewModels.ResultViews;

namespace TodoList.Proj.ExtensionMethods;

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