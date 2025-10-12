using System.Linq.Expressions;

namespace Bank.Management.Domain.Accounts.Specifications;

public class AccountNumberSpecification(string accountNumber) : SpecificationBase<Account, int>
{
    public override Expression<Func<Account, bool>> Criteria => account => account.AccountNumber == accountNumber;
}