namespace Bank.Management.Domain.Transactions.Rules;

public class TransactionMustBeUniqueRule(Guid transactionId, Func<Guid, bool> transactionExists) : IBusinessRule
{
    private const string MessageTemplate =  "Transaction with ID '{0}' already exists.";
    public bool IsBroken() => transactionExists(transactionId);

    public string Message => string.Format(MessageTemplate, transactionId);
}