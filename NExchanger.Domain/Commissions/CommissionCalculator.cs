namespace NExchanger.Domain.Commissions;

public class CommissionCalculator : ICommissionCalculator
{
    public decimal GetCommission(decimal destinationAmount, double commissionRate)
    {
        return destinationAmount * (decimal)commissionRate;
    }
}