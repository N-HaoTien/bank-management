namespace Bank.Management.Domain.Accounts.Rules;

public class SufficientBalanceRule(decimal currentBalance, decimal withdrawAmount) : IBusinessRule
{
    private const string MessageTemplate = "Insufficient funds. Current balance: {0}, requested: {1}.";

    public bool IsBroken() => withdrawAmount > currentBalance;

    public string Message => string.Format(MessageTemplate, currentBalance, withdrawAmount);
}