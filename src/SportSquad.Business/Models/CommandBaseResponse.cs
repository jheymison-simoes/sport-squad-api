namespace SportSquad.Business.Models;

public class CommandBaseResponse<T>
{
    public bool Error { get; }
    public string[] ErrorMessages { get; }
    public T Result { get; }
    public string StackTrace { get; }
    
    public CommandBaseResponse(T result, bool error = false, string[] errorMessages = null, string stackTrace = null)
    {
        Error = error;
        ErrorMessages = errorMessages;
        Result = result;
        StackTrace = stackTrace;
    }
}