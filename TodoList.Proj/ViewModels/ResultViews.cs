namespace ViewModels.ResultViews;

// class that work this to generate a easy Data output 

public class ResultViews<T>
{
    public ResultViews(T data)
    {
        Data = data;
    }

    public ResultViews(string errors)
    {
        Errors.Add(errors);
    }

    public ResultViews(List<string> errors)
    {
        Errors = errors;
    }

    public ResultViews(T data, List<string> errors)
    {
        Errors = errors;
        Data = data;
    }
    
    public T Data { get; set; }
    public List<string> Errors { get; set; }
}