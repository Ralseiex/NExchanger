using Microsoft.AspNetCore.Mvc;
using NExchanger.Services.Accounts;
using NExchanger.Services.Users;

namespace NExchanger.Api.Users;

public static class UsersApi
{
    public static void MapUsersApi(this WebApplication app)
    {
        var usersApi = app.MapGroup("/users").WithOpenApi().WithTags("Users");
        
        usersApi.MapPost("/", async (
            HttpContext context,
            [FromBody] CreateUserRequest request,
            [FromServices] IUserService service) =>
        {
            try
            {
                var newUserId = await service.CreateUser(request.UserName, request.Email, context.RequestAborted);
                return Results.Ok(newUserId);
            }
            catch (UsernameTakenException ex)
            {
                return Results.Conflict(new ErrorResponse("Username already taken"));
            }
        });
        
        usersApi.MapGet("/", async (HttpContext context, [FromServices] IUserService service) =>
        {
            var users = await service.GetAllUsers(context.RequestAborted);
            return Results.Ok(users);
        });
        
        usersApi.MapGet("/{userName}",
            async (string userName, HttpContext context, [FromServices] IUserService service) =>
            {
                try
                {
                    var users = await service.GetUser(userName, context.RequestAborted);
                    return Results.Ok(users);
                }
                catch (UserNotFoundException ex)
                {
                    return Results.NotFound(new ErrorResponse("Username not found"));
                }
            });
        
        usersApi.MapGet("/{userName}/account",
            async (string userName, HttpContext context, [FromServices] IAccountService service) =>
            {
                try
                {
                    var newAccountId = await service.GetAllUserAccounts(userName, context.RequestAborted);
                    return Results.Ok(newAccountId);
                }
                catch (UserNotFoundException ex)
                {
                    return Results.NotFound(new ErrorResponse("Username not found"));
                }
            });
    }
}