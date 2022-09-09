using System.Linq.Expressions;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;

public class SecondGetDto : BaseGet<SecondEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
    public override Expression<Func<SecondEntity, bool>> GetExpression(Expression<Func<SecondEntity, bool>> exp)
    {
        return base.GetExpression(exp)
        .AndIf(Id.HasValue, item => item.Id == Id)
        .AndIf(Name, item => item.Name == Name)
        .AndIf(Description, item => item.Description == Description)
        .AndIf(Age.HasValue, item => item.Age == Age)
        ;
    }
}