namespace Domain.CustomExceptions;

public class IdentityException : Exception
{
    public readonly int StatusCode;

    public IdentityException(string message) : base(message)
    {
    }
    
    public IdentityException(string resource ,int statusCode) : base(GetMessageByStatusCode(resource, statusCode))
    {
        StatusCode = statusCode;
    }

    private static string GetMessageByStatusCode(string resource, int statusCode)
    {
        return statusCode switch
        {
            401 => $"{resource} is Unauthorized",
            403 => $"Access to {resource} is forbidden",
            404 => $"{resource} not found",
            _ => $"Invalid {resource}"
        };
    }
}
