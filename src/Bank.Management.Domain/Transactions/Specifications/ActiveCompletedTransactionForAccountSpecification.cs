using System.Linq.Expressions;

namespace Bank.Management.Domain.Transactions.Specifications;

public class ActiveCompletedTransactionForAccountSpecification(Guid transactionId, int fromAccount, int toAccount)
    : ActiveTransitionTransactionForAccountSpecification(fromAccount, toAccount)
{
    public override Expression<Func<Transaction, bool>> Criteria
    {
        get
        {
            Expression<Func<Transaction, bool>> inProgressCondition =
                t => t.Id != transactionId
                     && t.Status == TransactionStatus.InProgress;

            return base.And(inProgressCondition);
        }
    }
}