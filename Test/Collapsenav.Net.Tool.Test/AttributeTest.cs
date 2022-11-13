using System;
using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class AttributeTest
{
    [Fact]
    public void HasAttributeTest()
    {
        Assert.True(typeof(PropTest1).HasAttribute<UnitTestAttribute>());
        Assert.False(typeof(PropTest0).HasAttribute<UnitTestAttribute>());
        Assert.True(typeof(PropTest1).HasAttribute(typeof(UnitTestAttribute)));
        Assert.False(typeof(PropTest0).HasAttribute(typeof(UnitTestAttribute)));

        Assert.True(typeof(PropTest1).Props().First().HasAttribute<UnitTestAttribute>());
        Assert.True(typeof(PropTest1).Props().First().HasAttribute(typeof(UnitTestAttribute)));
    }

    [Fact]
    public void AttrPropsTest()
    {
        Assert.NotEmpty(typeof(PropTest1).AttrProps<UnitTestAttribute>());
        Assert.Empty(typeof(PropTest0).AttrProps<UnitTestAttribute>());

        Assert.Equal(2, typeof(PropTest1).AttrProps<UnitTestAttribute>().Count());
    }

    [Fact]
    public void AttrFieldsTest()
    {
        Assert.NotEmpty(typeof(PropTest1).AttrFields<UnitTestAttribute>());
        Assert.Empty(typeof(PropTest0).AttrFields<UnitTestAttribute>());

        Assert.Equal(1, typeof(PropTest1).AttrFields<UnitTestAttribute>().Count());
    }

    [Fact]
    public void AttrMemberTest()
    {
        Assert.NotEmpty(typeof(PropTest1).AttrMembers<UnitTestAttribute>());
        Assert.Empty(typeof(PropTest0).AttrMembers<UnitTestAttribute>());

        Assert.Equal(3, typeof(PropTest1).AttrMembers<UnitTestAttribute>().Count());
    }

    [Fact]
    public void GetAttrFieldValueTest()
    {
        var attrValues = typeof(PropTest1).AttrFieldValues<UnitTestAttribute>();
        Assert.True(attrValues.Count == 1);
        Assert.True(attrValues.Select(item => item.Value.Field).AllContain("999"));
    }

    [Fact]
    public void GetAttrPropValueTest()
    {
        var attrValues = typeof(PropTest1).AttrPropValues<UnitTestAttribute>();
        Assert.True(attrValues.Count == 2);
        Assert.True(attrValues.Select(item => item.Value.Field).AllContain("123", "233"));
    }

    [Fact]
    public void GetAttrTest()
    {
        var attrValues = typeof(PropTest1).AttrValues<UnitTestAttribute>();
        Assert.True(attrValues.Count == 2);
        Assert.True(attrValues.Select(item => item.Value.Field).AllContain("123", "233"));
    }

    [Fact]
    public void GetAttrMemberValueTest()
    {
        var attrValues = typeof(PropTest1).AttrMemberValues<UnitTestAttribute>();
        Assert.True(attrValues.Count == 3);
        Assert.True(attrValues.Select(item => item.Value.Field).AllContain("123", "233", "999"));
    }

    [Fact]
    public void GetAttrPropTypeTest()
    {
        var types = typeof(PropTest2).AttrPropTypes<UnitTestAttribute>();
        Assert.True(types.AllContain(typeof(int), typeof(string)));
        Assert.Equal(2, types.Count());
    }

    [Fact]
    public void GetAttrFiledTypeTest()
    {
        var types = typeof(PropTest2).AttrFieldTypes<UnitTestAttribute>();

        Assert.True(types.AllContain(typeof(DateTime), typeof(double)));
        Assert.Equal(2, types.Count());
    }

    [Fact]
    public void GetAttrMemberTypeTest()
    {
        var types = typeof(PropTest2).AttrMemberTypes<UnitTestAttribute>();

        Assert.True(types.AllContain(typeof(int), typeof(string), typeof(DateTime), typeof(double)));
        Assert.Equal(4, types.Count());
    }
}