using Bank.Management.Domain.Accounts;
using Bank.Management.Domain.Transactions.DomainEvents;
using Bank.Management.Domain.Transactions.Rules;

namespace Bank.Management.Domain.Transactions;

public class Transaction : AuditEntityBase<Guid>, IDomainEvent
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }

    public Account FromAccount { get; set; } = null!;

    public Account ToAccount { get; set; } = null!;

    public TransactionStatus Status { get; private set; }
    
    public string ReasonFailed { get; private set; }

    // For EF
    protected Transaction()
    {
        Id = Guid.NewGuid();
    }

    public Transaction(int fromAccountId, int toAccountId, decimal amount) : this()
    {
        CheckRule(new TransactionAccountsMustBeDifferentRule(fromAccountId, toAccountId));
        CheckRule(new TransactionAmountMustBePositiveRule(amount));

        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
        CreatedAt = DateTime.UtcNow;
        MarkInProgress();
    }

    private void MarkInProgress()
    {
        Status = TransactionStatus.InProgress;
    }

    public void MarkCompleted()
    {
        CheckRule(new TransactionMustBeInProgressBeforeCompletionRule(Status));
        Status = TransactionStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new MarkCompletedDomainEvent(Id));
    }

    public void MarkFailed(string reason)
    {
        Status = TransactionStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
        ReasonFailed = reason;
    }
}