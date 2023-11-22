using Microsoft.EntityFrameworkCore;
using NExchanger.Api.Modules;
using NExchanger.Persistence;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddDbContext<NExchangerContext>(options => options.UseInMemoryDatabase("NExchanger"));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddServices();
    builder.Services.AddTransactions();
}

var app = builder.Build();
app.StartTransactionListener();

var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<NExchangerContext>();
await dbContext.PopulateFakeData();
scope.Dispose();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapApi();
}

app.Run();