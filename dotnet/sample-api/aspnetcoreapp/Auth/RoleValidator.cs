using System.Text.Json;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;

namespace aspnetcoreapp.Auth;

public class RoleValidator(): IRoleValidator
{
    public async Task<ValidateRoleResult> ValidateRolesAsync(string[] rolesToBeValidated)
    {
        var apiUrl = Environment.GetEnvironmentVariable("FGA_API_URL") ?? "http://localhost:8080";
        var configuration = new ClientConfiguration {
            ApiUrl = apiUrl,
            StoreId = "01KCW0SBH7EN04PJ70FKM4T181"
        };
        var fgaClient = new OpenFgaClient(configuration);
        
        var body = new ClientCheckRequest {
            User = "user:anne2",
            Relation = "can_write",
            Object = "brand:benny",
        };
        var response = await fgaClient.Check(body);
        return ValidateRoleResult.Success();
    }
}