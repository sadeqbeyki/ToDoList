using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppFramework.Domain;

public interface IRepository<TKey, T> where T : class
{
    Task<T> GetAsync(TKey id);
    Task<List<T>> GetAllAsync();
    IQueryable<T> GetAll();
    Task Create(T Entity);
    Task<bool> Exists(Expression<Func<T, bool>> expresstion);
    Task SaveChangesAsync();
    Task Delete(TKey id);

}
