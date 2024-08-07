using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Interfaces;


public interface IRepository<model_type> where model_type: BaseModel
{
    IEnumerable<model_type> GetAll();
    model_type? GetById(int id);
    model_type? GetById(Guid id);
    model_type Add(model_type model);
    model_type Update(model_type model);
    void Remove(model_type model);
    void Save();
}
