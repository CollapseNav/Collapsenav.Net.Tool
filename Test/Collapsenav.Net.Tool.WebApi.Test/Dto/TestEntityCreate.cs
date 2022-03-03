namespace Collapsenav.Net.Tool.WebApi.Test;
public class TestEntityCreate : BaseCreate<TestEntity>
{
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public TestEntityCreate(string code, int? number, bool? isTest)
    {
        Code = code;
        Number = number;
        IsTest = isTest;
    }
}

public class TestQueryEntityCreate : BaseCreate<TestQueryEntity>
{
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public TestQueryEntityCreate(string code, int? number, bool? isTest)
    {
        Code = code;
        Number = number;
        IsTest = isTest;
    }
}

public class TestModifyEntityCreate : BaseCreate<TestModifyEntity>
{
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public TestModifyEntityCreate(string code, int? number, bool? isTest)
    {
        Code = code;
        Number = number;
        IsTest = isTest;
    }
}



public class TestNotBaseEntityCreate : BaseCreate<TestNotBaseEntity>
{
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public TestNotBaseEntityCreate(string code, int? number, bool? isTest)
    {
        Code = code;
        Number = number;
        IsTest = isTest;
    }
}

public class TestNotBaseQueryEntityCreate : BaseCreate<TestNotBaseQueryEntity>
{
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public TestNotBaseQueryEntityCreate(string code, int? number, bool? isTest)
    {
        Code = code;
        Number = number;
        IsTest = isTest;
    }
}

public class TestNotBaseModifyEntityCreate : BaseCreate<TestNotBaseModifyEntity>
{
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public TestNotBaseModifyEntityCreate(string code, int? number, bool? isTest)
    {
        Code = code;
        Number = number;
        IsTest = isTest;
    }
}
