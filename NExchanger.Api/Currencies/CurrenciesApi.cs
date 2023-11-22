using Microsoft.AspNetCore.Mvc;
using NExchanger.Services.Currencies;

namespace NExchanger.Api.Currencies;

public static class CurrenciesApi
{
    public static void MapCurrenciesApi(this WebApplication app)
    {
        var currencyApi = app.MapGroup("/currencies").WithOpenApi().WithTags("Currencies");;
        currencyApi.MapPost("/", async (
            HttpContext context,
            [FromBody] CreateCurrencyRequest request,
            [FromServices] ICurrencyService service) =>
        {
            try
            {
                var newUserId = await service.CreateCurrency(request.Code, request.FullName, context.RequestAborted);
                return Results.Ok(newUserId);
            }
            catch (CurrencyAlreadyExistsException ex)
            {
                return Results.Conflict(new ErrorResponse("Currency already exists"));
            }
        });
        currencyApi.MapGet("/", async (HttpContext context, [FromServices] ICurrencyService service) =>
        {
            var users = await service.GetAllCurrency(context.RequestAborted);
            return Results.Ok(users);
        });
        currencyApi.MapGet("/{code}", async (string code, HttpContext context, [FromServices] ICurrencyService service) =>
        {
            try
            {
                var users = await service.GetCurrency(code, context.RequestAborted);
                return Results.Ok(users);
            }
            catch (CurrencyNotFoundException ex)
            {
                return Results.NotFound(new ErrorResponse("Currency not found"));
            }
        });
    }
}