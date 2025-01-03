using System.Linq.Expressions;
using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Specifications;

public class Specification<TModel> where TModel : BaseModel
{
    public Expression<Func<TModel, bool>>? Criteria { get; }
    public List<Expression<Func<TModel, object>>> Includes { get; } = new();
    public Expression<Func<TModel, object>>? OrderBy { get; set; }

    public Specification() { }
    public Specification(Expression<Func<TModel, bool>> criteria) => Criteria = criteria;
}

public class Specification<TModel, TResult> : Specification<TModel> where TModel : BaseModel
{
    public Specification() : base() { }
    public Specification(Expression<Func<TModel, bool>> criteria) : base(criteria) { }
    public Expression<Func<TModel, TResult>>? Selector { get; set; }
}
