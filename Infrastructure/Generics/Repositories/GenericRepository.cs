using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Generics;
using System.Linq.Expressions;

namespace Pharmacy.Infrastructure.Generics.Repositories;


public class GenericRepository<TModel> : IRepository<TModel> where TModel : BaseModel
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TModel> _dbSet;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TModel>();
    }
    public async virtual Task<IEnumerable<TModel>> GetAll() =>
        await _dbSet.ToListAsync();
    public async virtual Task<IEnumerable<TModel>> Filter(Expression<Func<TModel, bool>> filter) =>
        await _dbSet.Where(filter).ToListAsync();
    public async virtual Task<TModel?> GetById<TId>(TId id) =>
        await _dbSet.FindAsync(id);
    public async virtual Task<TModel> Add(TModel model) =>
        (await _dbSet.AddAsync(model)).Entity;
    public virtual TModel Update(TModel model) =>
        _dbSet.Update(model).Entity;
    public virtual void Delete(TModel model) =>
        _dbSet.Remove(model);
}
