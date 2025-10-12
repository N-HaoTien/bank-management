using Bank.Management.Domain.Accounts;
using Bank.Management.Domain.Accounts.Specifications;
using Bank.Management.Domain.Transactions;
using Bank.Management.Domain.Transactions.Specifications;

namespace Bank.Management.Domain.DomainServices;

public class TransactionManager(
    IAccountRepository accountRepository,
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<Guid> CreateAsync(int fromAccountId,
        string toAccountNumber,
        decimal amount, CancellationToken cancellationToken = default)
    {
        var fromAccount = await accountRepository.GetByIdAsync(fromAccountId, cancellationToken)
                          ?? throw new InvalidOperationException($"Sender account {fromAccountId} not found");

        var toAccount =
            await accountRepository.GetOneBySpecification(new AccountNumberSpecification(toAccountNumber),
                cancellationToken)
            ?? throw new InvalidOperationException($"Receiver account number {toAccountNumber} not found");

        fromAccount.CheckWithDrawRule(amount);

        var transaction = new Transaction(fromAccountId, toAccount.Id, amount);

        var id = await transactionRepository.CreateAsync(transaction, cancellationToken);

        await unitOfWork.SaveChangeAsync();

        return id;
    }


    public async Task<bool> MarkCompletedAsync(Guid id,
        decimal amount, CancellationToken cancellationToken = default)
    {
        var transaction = await transactionRepository.GetByIdAsync(id, cancellationToken)
                          ?? throw new InvalidOperationException(
                              $"Receiver account number {id} not found");

        var spec = new ActiveCompletedTransactionForAccountSpecification(transaction.Id, transaction.FromAccountId,
            transaction.ToAccountId);

        var hasActiveTransition = await transactionRepository.HasActiveTransitionAsync(spec, cancellationToken);

        if (!hasActiveTransition)
        {
            return false;
        }

        transaction.MarkCompleted();

        await unitOfWork.SaveChangeAsync();

        return true;
    }

    public async Task MarkFailedTransactionAsync(Guid id,
        string reason, CancellationToken cancellationToken = default)
    {
        var transaction = await transactionRepository.GetByIdAsync(id, cancellationToken)
                          ?? throw new InvalidOperationException(
                              $"Receiver account number {id} not found");

        transaction.MarkFailed("");

        await unitOfWork.SaveChangeAsync();
    }
}