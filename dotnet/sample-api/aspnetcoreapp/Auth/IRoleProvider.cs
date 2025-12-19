namespace aspnetcoreapp.Auth;

public interface IRoleProvider
{
    Task<GetRolesResult> GetRolesAsync(string token);
}