using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TabloRazor.Components.QuickTables.Infrastructure;

namespace TabloRazor.QuickTable.EntityFramework;

internal class EntityFrameworkAsyncQueryExecutor : IAsyncQueryExecutor
{
    public bool IsSupported<T>(IQueryable<T> queryable)
        => queryable.Provider is IAsyncQueryProvider;

    public Task<int> CountAsync<T>(IQueryable<T> queryable)
        => queryable.CountAsync();

    public Task<T[]> ToArrayAsync<T>(IQueryable<T> queryable)
        => queryable.ToArrayAsync();
}