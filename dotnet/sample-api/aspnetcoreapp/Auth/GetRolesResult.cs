namespace aspnetcoreapp.Auth;

public class GetRolesResult
{
    private GetRolesResult(bool isSuccess, string[]? errorMessages, string[]? roles = null)
    {
        IsSuccess = isSuccess;
        ErrorMessages = errorMessages;
        Roles = roles;
    }

    public bool IsSuccess { get; }
    public string[]? ErrorMessages { get; }
    public string[]? Roles { get; }

    public static GetRolesResult Success(string[] roles)
    {
        ArgumentNullException.ThrowIfNull(roles, nameof(roles));
        return new GetRolesResult(isSuccess: true, errorMessages: null, roles: roles);
    }

    public static GetRolesResult Failure(string[] errorMessages)
    {
        ArgumentNullException.ThrowIfNull(errorMessages, nameof(errorMessages));
        return new GetRolesResult(isSuccess: true, errorMessages: errorMessages, roles: null);
    }
}