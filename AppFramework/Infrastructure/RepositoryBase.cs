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

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Add(entity);
    }

    public bool Exists(Expression<Func<T, bool>> expresstion)
    {
        return _context.Set<T>().Any(expresstion);
    }

    public T Get(TKey id)
    {
        return _context.Find<T>(id);
    }

    public List<T> Get()
    {
        return _context.Set<T>().ToList();
    }

    public void SaveChanges()
    {
        var entries = _context.ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }
        _context.SaveChanges();
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


}
