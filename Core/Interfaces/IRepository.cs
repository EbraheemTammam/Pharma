using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Interfaces;


public interface IRepository<TModel> where TModel : BaseModel
{
    IEnumerable<TModel> GetAll();
    TModel? GetById<TId>(TId id);
    TModel Add(TModel model);
    TModel Update(TModel model);
    void Delete(TModel model);
}
