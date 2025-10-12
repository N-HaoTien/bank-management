using Bank.Management.Domain.Accounts.Rules;

namespace Bank.Management.Domain.Accounts;

public class Account : AuditEntityBase<int>
{
    public string AccountNumber { get; private set; }
    public string UserName { get; private set; }
    public decimal Balance { get; private set; }

    // For EF
    protected Account()
    {
    }

    public Account(string accountNumber, string userName)
    {
        CheckRule(new AccountNumberMustBeNotNullRule(accountNumber));
        CheckRule(new UserNameMustBeNotNullRule(userName));

        Balance = 0M;
        AccountNumber = accountNumber;
        UserName = userName;
    }

    public void Withdraw(decimal amount)
    {
        CheckWithDrawRule(amount);

        Balance -= amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CheckWithDrawRule(decimal amount)
    {
        CheckRule(new AmountMustBePositiveRule(amount));
        CheckRule(new SufficientBalanceRule(Balance, amount));
    }

    public void Deposit(decimal amount)
    {
        CheckRule(new DepositAmountMustBePositiveRule(amount));
        Balance += amount;

        UpdatedAt = DateTime.UtcNow;
    }
}