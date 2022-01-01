using System;
using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class TestEntityGet : BaseGet<TestEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override Expression<Func<TestEntity, bool>> GetExpression(Expression<Func<TestEntity, bool>> exp)
    {
        return base.GetExpression(exp)
        .AndIf(Id.HasValue, item => item.Id == Id)
        .AndIf(Code, item => item.Code.Contains(Code))
        .AndIf(Number.HasValue, item => item.Number > Number)
        .AndIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
public class TestQueryEntityGet : BaseGet<TestQueryEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override Expression<Func<TestQueryEntity, bool>> GetExpression(Expression<Func<TestQueryEntity, bool>> exp)
    {
        return base.GetExpression(exp)
        .AndIf(Id.HasValue, item => item.Id == Id)
        .AndIf(Code, item => item.Code.Contains(Code))
        .AndIf(Number.HasValue, item => item.Number > Number)
        .AndIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}

public class TestModifyEntityGet : BaseGet<TestModifyEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override Expression<Func<TestModifyEntity, bool>> GetExpression(Expression<Func<TestModifyEntity, bool>> exp)
    {
        return base.GetExpression(exp)
        .AndIf(Id.HasValue, item => item.Id == Id)
        .AndIf(Code, item => item.Code.Contains(Code))
        .AndIf(Number.HasValue, item => item.Number > Number)
        .AndIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
