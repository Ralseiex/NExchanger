using Microsoft.AspNetCore.Mvc;
using NExchanger.Services.Accounts;
using NExchanger.Services.Currencies;
using NExchanger.Services.Users;

namespace NExchanger.Api.Accounts;

public static class AccountApi
{
    public static void MapAccountApi(this WebApplication app)
    {
        var accountApi = app.MapGroup("/accounts").WithOpenApi().WithTags("Accounts");
        accountApi.MapPost("/", async (
            HttpContext context,
            [FromBody] CreateAccountRequest request,
            [FromServices] IAccountService service) =>
        {
            try
            {
                var newAccountId =
                    await service.CreateAccount(request.OwnerName, request.CurrencyCode, context.RequestAborted);
                return Results.Ok(newAccountId);
            }
            catch (UserNotFoundException ex)
            {
                return Results.NotFound(new ErrorResponse("Username not found"));
            }
            catch (CurrencyNotFoundException ex)
            {
                return Results.NotFound(new ErrorResponse("Currency not found"));
            }
        });

        accountApi.MapGet("/{id}", async (int id, HttpContext context, [FromServices] IAccountService service) =>
        {
            try
            {
                var users = await service.GetAccount(id, context.RequestAborted);
                return Results.Ok(users);
            }
            catch (AccountNotFoundException ex)
            {
                return Results.NotFound(new ErrorResponse("Account not found"));
            }
        });
    }
}