namespace Bank.Management.Domain.Transactions.Rules;

public class TransactionMustBeInProgressBeforeCompletionRule(TransactionStatus currentStatus) : IBusinessRule
{
    private const string MessageTemplate =
        "Transaction must be in progress before it can be marked as completed or failed.";

    public bool IsBroken() => currentStatus == TransactionStatus.InProgress;

    public string Message => MessageTemplate;
}