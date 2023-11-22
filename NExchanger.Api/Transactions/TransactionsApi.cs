using Microsoft.AspNetCore.Mvc;
using NExchanger.Services.Accounts;
using NExchanger.Services.Transactions;
using NExchanger.Services.Users;

namespace NExchanger.Api.Transactions;

public static class TransactionsApi
{
    public static void MapTransactionsApi(this WebApplication app)
    {
        var transactionsApi = app.MapGroup("/transactions").WithOpenApi().WithTags("Transactions");
        transactionsApi.MapPost("/", async (
            HttpContext context,
            [FromBody] CreateTransactionRequest request,
            [FromServices] ITransactionService service) =>
        {
            try
            {
                var newUserId = await service.CreateTransaction(
                    request.SourceAccountId,
                    request.DestinationAccountId,
                    request.Amount,
                    request.ExchangeRate,
                    request.Commission,
                    context.RequestAborted);
                return Results.Ok(newUserId);
            }
            catch (AccountNotFoundException ex)
            {
                return Results.NotFound(new ErrorResponse("Account not found"));
            }
            catch (InvalidTransactionException ex)
            {
                return Results.BadRequest(new ErrorResponse(ex.Message));
            }
        });

        transactionsApi.MapGet("/", async (HttpContext context, [FromServices] ITransactionService service) =>
        {
            var users = await service.GetAllTransaction(context.RequestAborted);
            return Results.Ok(users);
        });

        transactionsApi.MapGet("/{id}", async (
            int id,
            HttpContext context,
            [FromServices] ITransactionService service) =>
        {
            try
            {
                var users = await service.GetTransaction(id, context.RequestAborted);
                return Results.Ok(users);
            }
            catch (UserNotFoundException ex)
            {
                return Results.NotFound(new ErrorResponse("Username not found"));
            }
        });
    }
}