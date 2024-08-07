using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Generics;

namespace Pharmacy.Infrastructure.Generics.Repositories;


public class GenericRepository<model_type> : IRepository<model_type> where model_type : BaseModel
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<model_type> _dbSet;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<model_type>();
    }
    public IEnumerable<model_type> GetAll() => _dbSet.ToList();
    public model_type? GetById(int id) => _dbSet.Find(id);
    public model_type? GetById(Guid id) => _dbSet.Find(id);
    public model_type Add(model_type model) => _dbSet.Add(model).Entity;
    public model_type Update(model_type model) => _dbSet.Update(model).Entity;
    public void Remove(model_type model) => _dbSet.Remove(model);
    public void Save() => _context.SaveChanges();
}
