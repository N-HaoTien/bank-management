namespace Bank.Management.Domain.Transactions.Rules;

public class TransactionAccountsMustBeDifferentRule(int fromAccountId, int toAccountId) : IBusinessRule
{
    private const string MessageTemplate = "Deposit amount must be greater than zero.";
    public bool IsBroken() => fromAccountId == toAccountId;

    public string Message => MessageTemplate;
}