using System.Linq.Expressions;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;

public class FirstGetDto : BaseGet<FirstEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public override Expression<Func<FirstEntity, bool>> GetExpression(Expression<Func<FirstEntity, bool>> exp)
    {
        return base.GetExpression(exp)
        .AndIf(Id, item => item.Id == Id)
        .AndIf(Name, item => item.Name == Name)
        .AndIf(Description, item => item.Description == Description)
        ;
    }
}