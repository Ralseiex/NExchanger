namespace NExchanger.Domain.Commissions;

public interface ICommissionCalculator
{
    decimal GetCommission(decimal destinationAmount, double commissionRate);
}