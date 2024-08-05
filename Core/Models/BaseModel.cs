using Pharmacy.Domain.Interfaces;

namespace Pharmacy.Domain.Models;


public abstract class BaseModel
{

}

public abstract class BaseModel<id_type>: BaseModel where id_type: struct
{
    public id_type Id {get;}
}
