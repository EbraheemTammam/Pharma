using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Generics;

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
    public virtual IEnumerable<TModel> GetAll() => _dbSet.ToList();
    public virtual TModel? GetById<TId>(TId id) => _dbSet.Find(id);
    public virtual TModel Add(TModel model) => _dbSet.Add(model).Entity;
    public virtual TModel Update(TModel model) => _dbSet.Update(model).Entity;
    public virtual void Delete(TModel model) => _dbSet.Remove(model);
}
