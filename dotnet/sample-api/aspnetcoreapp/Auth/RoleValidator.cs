using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;

namespace aspnetcoreapp.Auth;

public class RoleValidator(): IRoleValidator
{
    public async Task<ValidateRoleResult> ValidateRolesAsync(string[] rolesToBeValidated)
    {
        var apiUrl = Environment.GetEnvironmentVariable("FGA_API_URL") ?? "http://localhost:8080";
        var storeId = Environment.GetEnvironmentVariable("FGA_STORE_ID") ?? "default";
        
        var configuration = new ClientConfiguration {
            ApiUrl = apiUrl,
            StoreId = storeId
        };
        var fgaClient = new OpenFgaClient(configuration);
        
        var store = await fgaClient.CreateStore(new ClientCreateStoreRequest { Name = "POC" });
        return ValidateRoleResult.Success();
    }
}