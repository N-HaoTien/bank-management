namespace Bank.Management.Domain;

public interface IUnitOfWork
{
    Task SaveChangeAsync();
    
    Task CommitAsync();
}