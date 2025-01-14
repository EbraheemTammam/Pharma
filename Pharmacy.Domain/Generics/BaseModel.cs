namespace Pharmacy.Domain.Generics;


public interface BaseModel
{

}

public abstract class BaseModel<id_type> : BaseModel where id_type : struct
{
    public id_type Id {get;}
}
