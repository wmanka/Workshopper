using Microsoft.EntityFrameworkCore;
using Workshopper.Application.Common.Models;
using Workshopper.Domain.Common;

namespace Workshopper.Infrastructure.Common.Persistence;

internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, Specification<TEntity> specification)
        where TEntity : DomainEntity
    {
        var query = inputQuery;

        if (specification.Filter is not null)
        {
            query = query.Where(specification.Filter);
        }

        query = specification.Include
            .Aggregate(query,
                (current, include) =>
                {
                    return current.Include(include);
                });

        if (specification.Sort is not null)
        {
            query = query.OrderBy(specification.Sort);
        }

        return query;
    }
}