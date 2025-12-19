namespace aspnetcoreapp.Auth;

public class RoleProvider: IRoleProvider
{
    public Task<GetRolesResult> GetRolesAsync(string token)
    {
        return Task.FromResult(GetRolesResult.Success(["Admin"]));
    }
}