namespace Bank.Management.Domain.Accounts.Rules;

public class UserNameMustBeNotNullRule(string userName) : IBusinessRule
{
    private const string MessageTemplate = "UserName must be not null.";
    public bool IsBroken() => string.IsNullOrWhiteSpace(userName);
    public string Message => MessageTemplate;
}