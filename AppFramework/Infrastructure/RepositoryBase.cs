using AppFramework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppFramework.Infrastructure;

public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public RepositoryBase(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public IQueryable<T> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task Create(T entity)
    {
        await _context.AddAsync(entity);
    }

    public async Task<bool> Exists(Expression<Func<T, bool>> expresstion)
    {
        return await _context.Set<T>().AnyAsync(expresstion);
    }



    public async Task<T> GetAsync(TKey id)
    {
        return await _context.FindAsync<T>(id);
    }

    public async Task SaveChangesAsync()
    {
        var entries = _context.ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }
        await _context.SaveChangesAsync();
    }
    public void Delete(TKey id)
    {
        _context.Remove(id);
    }

}
