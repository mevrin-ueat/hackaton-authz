namespace aspnetcoreapp.Auth;

public interface IAuthService
{
    Task<AuthResult> AuthAsync(string token);
}