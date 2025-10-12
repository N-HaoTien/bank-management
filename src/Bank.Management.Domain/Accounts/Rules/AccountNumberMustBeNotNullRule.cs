namespace Bank.Management.Domain.Accounts.Rules;

public class AccountNumberMustBeNotNullRule(string accountNumber) : IBusinessRule
{
    private const string MessageTemplate = "Account Number must be not null.";
    public bool IsBroken() => string.IsNullOrWhiteSpace(accountNumber);
    public string Message => MessageTemplate;
}