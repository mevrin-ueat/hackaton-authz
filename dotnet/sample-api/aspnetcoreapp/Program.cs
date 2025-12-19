using aspnetcoreapp.Auth;

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

app.Run();
