namespace aspnetcoreapp.Auth;

public class ValidateRoleResult
{
    private ValidateRoleResult(bool isValid, string? errorMessages)
    {
        IsValid = isValid;
        ErrorMessages = errorMessages;
    }

    public bool IsValid { get; }
    public string? ErrorMessages { get; }


    public static ValidateRoleResult Success()
    {
        return new(isValid: true, errorMessages: null);
    }

    public static ValidateRoleResult Failure(string errorMessages)
    {
        ArgumentNullException.ThrowIfNull(errorMessages, nameof(errorMessages));
        return new(isValid: true, errorMessages: errorMessages);
    }
}