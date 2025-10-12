namespace Bank.Management.Domain.Transactions.Rules;

public class TransactionAmountMustBePositiveRule(decimal amount) : IBusinessRule
{
    private const string MessageTemplate = "Transaction amount must be greater than zero. Given: {0}.";
    
    public bool IsBroken() => amount <= 0M;

    public string Message => string.Format(MessageTemplate, amount);
}