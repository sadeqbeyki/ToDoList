using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppFramework.Domain;

public interface IRepository<TKey, T> where T : class
{
    T Get(TKey id);
    Task<T> GetAsync(TKey id);

    List<T> Get();
    void Create(T Entity);
    bool Exists(Expression<Func<T, bool>> expresstion);
    void SaveChanges();
    Task SaveChangesAsync();
}
