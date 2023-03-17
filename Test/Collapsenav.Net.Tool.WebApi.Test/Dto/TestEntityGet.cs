using System;
using System.Linq;
using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class TestEntityGet : BaseGet<TestEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override IQueryable<TestEntity> GetQuery(IQueryable<TestEntity> query)
    {
        return query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
public class TestQueryEntityGet : BaseGet<TestQueryEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override IQueryable<TestQueryEntity> GetQuery(IQueryable<TestQueryEntity> query)
    {
        return query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}

public class TestModifyEntityGet : BaseGet<TestModifyEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override IQueryable<TestModifyEntity> GetQuery(IQueryable<TestModifyEntity> query)
    {
        return query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}


public class TestNotBaseEntityGet : BaseGet<TestNotBaseEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override IQueryable<TestNotBaseEntity> GetQuery(IQueryable<TestNotBaseEntity> query)
    {
        return query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
public class TestNotBaseQueryEntityGet : BaseGet<TestNotBaseQueryEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override IQueryable<TestNotBaseQueryEntity> GetQuery(IQueryable<TestNotBaseQueryEntity> query)
    {
        return query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}

public class TestNotBaseModifyEntityGet : BaseGet<TestNotBaseModifyEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override IQueryable<TestNotBaseModifyEntity> GetQuery(IQueryable<TestNotBaseModifyEntity> query)
    {
        return query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
