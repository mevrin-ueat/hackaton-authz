namespace aspnetcoreapp.Auth;

public class AuthService(IRoleProvider roleProvider, IRoleValidator roleValidator): IAuthService
{
    public async Task<AuthResult> AuthAsync(string token)
    {
            // Use the token to get roles
            var getRolesResult = await roleProvider.GetRolesAsync(token);

            if (getRolesResult.IsSuccess)
            {
                if (getRolesResult.Roles is null or [])
                {
                    return AuthResult.Failure("No roles found");
                }

                var validateRoleResult = await roleValidator.ValidateRolesAsync(getRolesResult.Roles!);

                if (validateRoleResult.IsValid)
                {
                    return AuthResult.Success();
                }

                return AuthResult.Failure("Invalid roles");

            }
            
            return AuthResult.Failure("Failed to get roles");
    }
}