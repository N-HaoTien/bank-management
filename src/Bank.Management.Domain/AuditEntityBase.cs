namespace Bank.Management.Domain;

public class AuditEntityBase<T> : EntityBase<T>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}