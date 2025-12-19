namespace aspnetcoreapp.Auth;

public interface IRoleValidator
{
    Task<ValidateRoleResult> ValidateRolesAsync(string[] rolesToBeValidated);
}