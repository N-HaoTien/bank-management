namespace Bank.Management.Domain.Accounts.Rules;

public class AmountMustBePositiveRule(decimal amount) : IBusinessRule
{
    private const string MessageTemplate = "Amount must be greater than zero. Given: {0}.";
    public bool IsBroken() => amount <= 0M;
    public string Message => string.Format(MessageTemplate, amount);
}