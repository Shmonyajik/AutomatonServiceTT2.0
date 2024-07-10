using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Models;

namespace Warehouse.DAL.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<Storage> IncludeDetails(this IQueryable<Storage> queryable, bool include = true)
    {
        if (!include) return queryable;

        return queryable
            .Include(s => s.Products)
            .Include(s => s.Employees);
    }
}