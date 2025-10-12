using System.Linq.Expressions;

namespace Bank.Management.Domain.Transactions.Specifications;

public class ActiveTransitionTransactionForAccountSpecification(int fromAccount, int toAccount)
    : SpecificationBase<Transaction, Guid>
{
    public override Expression<Func<Transaction, bool>> Criteria
        => transaction => (transaction.FromAccountId == fromAccount || transaction.ToAccountId == fromAccount) &&
                          (transaction.FromAccountId == toAccount || transaction.ToAccountId == toAccount);
}