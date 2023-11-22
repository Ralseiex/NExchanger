using Microsoft.EntityFrameworkCore;
using NExchanger.Domain.Transactions;
using NExchanger.Persistence;
using NExchanger.Services.Accounts;
using TransactionEntity = NExchanger.Persistence.Entities.Transaction;

namespace NExchanger.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly NExchangerContext _dbContext;
    private readonly ITransactionValidator _transactionValidator;
    private readonly ITransactionApplier _transactionApplier;

    public TransactionService(
        NExchangerContext dbContext, 
        ITransactionValidator transactionValidator, 
        ITransactionApplier transactionApplier)
    {
        _dbContext = dbContext;
        _transactionValidator = transactionValidator;
        _transactionApplier = transactionApplier;
    }

    public async Task<int> CreateTransaction(
        int sourceAccountId,
        int destinationAccountId,
        decimal amount,
        double exchangeRate,
        double commission,
        CancellationToken cancellationToken)
    {
        var sourceAccount = await _dbContext.Accounts
            .SingleOrDefaultAsync(account => account.Id == sourceAccountId, cancellationToken);
        if (sourceAccount is null) throw new AccountNotFoundException();
        var destinationAccount = await _dbContext.Accounts
            .SingleOrDefaultAsync(account => account.Id == destinationAccountId, cancellationToken);
        if (destinationAccount is null) throw new AccountNotFoundException();
        
        var transaction = Transaction.Create(sourceAccountId, destinationAccountId, amount, exchangeRate, commission);
        var validationResult = _transactionValidator.Validate(transaction);
        if (validationResult.IsValid == false)
            throw new InvalidTransactionException(string.Join(Environment.NewLine, validationResult.ErrorMessages));
        
        var transactionEntity = new TransactionEntity()
        {
            Amount = transaction.Amount,
            Commission = transaction.CommissionRate,
            ExchangeRate = transaction.ExchangeRate,
            DateTime = transaction.DateTime,
            Created = DateTime.UtcNow,
            DestinationAccount = destinationAccount,
            SourceAccount = sourceAccount
        };

        _dbContext.Transactions.Add(transactionEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _transactionApplier.SendTransaction(transaction.AddId(transactionEntity.Id));

        return transactionEntity.Id;
    }

    public async Task<TransactionDto> GetTransaction(int id, CancellationToken cancellationToken)
    {
        var transaction = await _dbContext.Transactions
            .SingleOrDefaultAsync(transaction => transaction.Id == id, cancellationToken);
        if (transaction is null) throw new TransactionNotFoundException();

        return new TransactionDto(
            transaction.DateTime,
            transaction.SourceAccountId,
            transaction.DestinationAccountId,
            transaction.Amount,
            transaction.ExchangeRate,
            transaction.Commission);
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransaction(CancellationToken cancellationToken)
    {
        var transactions = await _dbContext.Transactions
            .ToListAsync(cancellationToken);

        return transactions.Select(transaction => new TransactionDto(
            transaction.DateTime,
            transaction.SourceAccountId,
            transaction.DestinationAccountId,
            transaction.Amount,
            transaction.ExchangeRate,
            transaction.Commission));
    }
}