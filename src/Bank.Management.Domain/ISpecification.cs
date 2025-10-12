using System.Linq.Expressions;

namespace Bank.Management.Domain;

public interface ISpecification<T, TId> where T : EntityBase<TId>
{
    Expression<Func<T, bool>> Criteria { get; }

    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
}