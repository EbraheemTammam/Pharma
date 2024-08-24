using System.Linq.Expressions;
using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Interfaces;


public interface IRepository<TModel> where TModel : BaseModel
{
    Task<IEnumerable<TModel>> GetAll();
    Task<IEnumerable<TModel>> Filter(Expression<Func<TModel, bool>> func);
    Task<TModel?> GetById<TId>(TId id);
    Task<TModel> Add(TModel model);
    TModel Update(TModel model);
    void Delete(TModel model);
}
