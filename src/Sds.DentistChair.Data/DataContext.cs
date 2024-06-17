﻿using Microsoft.EntityFrameworkCore;
using Sds.DentistChair.Domain.Repository;
using System.Linq.Expressions;

namespace Sds.DentistChair.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options), IRepository
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public async Task<bool> CommitAsync()
        => await base.SaveChangesAsync() > 0;

    public IQueryable<T> QueryAsNoTracking<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
    {
        var query = base.Set<T>().AsQueryable().AsNoTrackingWithIdentityResolution();

        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return query;
    }

    public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
    {
        var query = base.Set<T>().AsQueryable();

        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return query;
    }

    public async Task AddAsync<T>(T entity) where T : class => await base.Set<T>().AddAsync(entity);

    public async Task AddRange<T>(IEnumerable<T> items) where T : class => await AddRangeAsync(items);

    public new void Remove<T>(T item) where T : class => base.Remove(item);

    public void RemoveRange<T>(IEnumerable<T> items) where T : class => base.RemoveRange(items);

    public IQueryable<T> QueryIncludeStringProperties<T>(params string[] includeProperties) where T : class
    {
        var query = base.Set<T>().AsQueryable();
        if (includeProperties is { Length: > 0 })
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return query;
    }

    public async Task BeginTransactionAsync() => await base.Database.BeginTransactionAsync();

    public void RollbackTransaction() => base.Database.RollbackTransaction();

    public void CommitTransaction() => base.Database.CommitTransaction();

    public new void Update<T>(T entity) where T : class => base.Set<T>().Update(entity);
}
