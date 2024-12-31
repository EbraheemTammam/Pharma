using Pharmacy.Domain.Generics;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Domain.Interfaces;


public interface IRepository<TEntity> where TEntity : BaseModel
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> GetAll(Specification<TEntity> specification);
    Task<IEnumerable<TResult>> GetAll<TResult>(Specification<TEntity, TResult> specification);
    Task<TEntity?> GetOne(Specification<TEntity> specification);
    Task<TResult?> GetOne<TResult>(Specification<TEntity, TResult> specification);
    Task<TEntity?> GetById<TId>(TId id);
    Task<TEntity> Add(TEntity model);
    TEntity Update(TEntity model);
    void Delete(TEntity model);
    Task Save();
}
