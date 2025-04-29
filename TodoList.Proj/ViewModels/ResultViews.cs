namespace ViewModels.ResultViews;


public class ResultViewsDataAndErrorsInJSON<T>
{
    public ResultViewsDataAndErrorsInJSON(T data)
    {
        Data = data;
    }

    public ResultViewsDataAndErrorsInJSON(string errors)
    {
        Errors.Add(errors);
    }

    public ResultViewsDataAndErrorsInJSON(List<string> errors)
    {
        Errors = errors;
    }

    public ResultViewsDataAndErrorsInJSON(T data, List<string> errors)
    {
        Errors = errors;
        Data = data;
    }
    
    public T Data { get; set; }
    public List<string> Errors { get; set; }
}