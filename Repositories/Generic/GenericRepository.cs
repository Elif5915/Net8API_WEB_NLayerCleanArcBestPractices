
using App_Repositories.Product;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App_Repositories.Generic;
public class GenericRepository<T, TId>(AppDbContext appDbContext) : IGenericRepository<T> where T : BaseEntity<TId>
{
    protected AppDbContext Context = appDbContext;  //protected ile bir sabit değişken tanımlayıp oraya attık context ve protected ile miras alan her yerde bu context sabit değişkeni kullanabilir demiş olduk. 

    private readonly DbSet<T> _dbSet = appDbContext.Set<T>();
    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id!.Equals(id));
    public async ValueTask AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    //public IQueryable<T> GetAll()
    //{
    //   return _dbSet.AsQueryable();
    //} 
    public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();
    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);
}
