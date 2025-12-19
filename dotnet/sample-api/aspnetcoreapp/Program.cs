using System.Text.Json;
using aspnetcoreapp.Auth;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleProvider, RoleProvider>();
builder.Services.AddScoped<IRoleValidator, RoleValidator>();

var app = builder.Build();

app.Use((async (context, func) =>
{
    if (!context.Request.Headers.TryGetValue("Authorization", out var token))
    {
        context.Response.StatusCode = 401;
        return;
    }

    var authService = context.RequestServices.GetRequiredService<IAuthService>();
    var authResult = await authService.AuthAsync(token);
    if (!authResult.IsAuthorized)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Forbidden: " + authResult.ErrorMessage);
        return;
    }

    await func();
}));

app.MapGet("/", () => "Hello World!");

app.MapPost("/create", async () =>
{
    var apiUrl = Environment.GetEnvironmentVariable("FGA_API_URL") ?? "http://localhost:8080";
    var configuration = new ClientConfiguration {
        ApiUrl = apiUrl,
        StoreId = "01KCW0SBH7EN04PJ70FKM4T181"
    };
    var fgaClient = new OpenFgaClient(configuration);
    // var store = await fgaClient.CreateStore(new OpenFga.Sdk.Client.Model.ClientCreateStoreRequest { Name = "POC" });
    // var modelPath = "C:\\Projects\\hackaton-authz\\openfga\\model.json";
    // if (!File.Exists(modelPath))
    //     throw new FileNotFoundException($"OpenFGA model file not found: {modelPath}");
    //
    // var modelJson = await File.ReadAllTextAsync(modelPath);
    //
    // var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    // var body = JsonSerializer.Deserialize<ClientWriteAuthorizationModelRequest>(modelJson, options)
    //            ?? throw new InvalidOperationException("Failed to deserialize OpenFGA model.json");
    //
    // await fgaClient.WriteAuthorizationModel(body);

    var body2 = new ClientWriteRequest() {
        Writes = new List<ClientTupleKey>() {
            new() {
                User = "user:anne2",
                Relation = "employee",
                Object = "brand:benny"
            }
        },
    };
    await fgaClient.Write(body2);
    return "store.Id";
}).AllowAnonymous();

app.Run();
