using System.Linq.Expressions;

namespace Bank.Management.Domain;

public abstract class SpecificationBase<T, TId> : ISpecification<T, TId> where T : EntityBase<TId>
{
    public abstract Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public Expression<Func<T, bool>> And(Expression<Func<T, bool>> other)
    {
        return CombineExpressions(Criteria, other, Expression.AndAlso);
    }

    public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> other)
    {
        return CombineExpressions(Criteria, other, Expression.OrElse);
    }

    private static Expression<Func<T, bool>> CombineExpressions(
        Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right,
        Func<Expression, Expression, BinaryExpression> merge)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftBody = Expression.Invoke(left, parameter);
        var rightBody = Expression.Invoke(right, parameter);

        var body = merge(leftBody, rightBody);

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}