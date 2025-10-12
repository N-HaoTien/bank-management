using Bank.Management.Domain.Transactions.Specifications;

namespace Bank.Management.Domain.Transactions;

public interface ITransactionRepository : IRepository<Transaction, Guid>
{
    Task<bool> HasActiveTransitionAsync(SpecificationBase<Transaction, Guid> spec,
        CancellationToken cancellationToken = default);
}