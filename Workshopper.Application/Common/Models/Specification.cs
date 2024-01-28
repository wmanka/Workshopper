using System.Linq.Expressions;
using Workshopper.Domain.Common;

namespace Workshopper.Application.Common.Models;

public abstract class Specification<TEntity>
    where TEntity : DomainEntity
{
    protected Specification()
    {
    }

    public Expression<Func<TEntity, bool>>? Filter { get; private set; }

    public List<Expression<Func<TEntity, object>>> Include { get; } = [];

    public Expression<Func<TEntity, object>>? Sort { get; private set; }

    protected void AddFilter(Expression<Func<TEntity, bool>> filterExpression)
    {
        Filter = filterExpression;
    }

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Include.Add(includeExpression);
    }

    protected void AddSort(Expression<Func<TEntity, object>> sortExpression)
    {
        Sort = sortExpression;
    }
}