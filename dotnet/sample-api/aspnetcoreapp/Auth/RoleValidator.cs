namespace aspnetcoreapp.Auth;

public class RoleValidator: IRoleValidator
{
    public Task<ValidateRoleResult> ValidateRolesAsync(string[] rolesToBeValidated)
    {
        return Task.FromResult(ValidateRoleResult.Success());
    }
}