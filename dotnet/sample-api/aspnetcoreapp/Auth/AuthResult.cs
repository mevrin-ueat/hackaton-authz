namespace aspnetcoreapp.Auth;

public class AuthResult
{
    private AuthResult(bool isAuthorized, string? errorMessage = null)
    {
        IsAuthorized = isAuthorized;
        ErrorMessage = errorMessage;
    }

    public bool IsAuthorized { get; }
    public string? ErrorMessage { get;}

    public static AuthResult Success() => new(true);
    public static AuthResult Failure(string errorMessage) => new(false, errorMessage);
}