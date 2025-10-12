namespace Bank.Management.Domain.Accounts.Rules;

public class DepositAmountMustBePositiveRule(decimal amount) : IBusinessRule
{
    private const string MessageTemplate = "Deposit amount must be greater than zero.";
    public bool IsBroken() => amount <= 0M;

    public string Message => MessageTemplate;
}