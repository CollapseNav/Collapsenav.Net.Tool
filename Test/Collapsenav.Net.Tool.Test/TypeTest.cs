using System.Collections;
using Xunit;

namespace Collapsenav.Net.Tool.Test;

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
sealed class UnitTestAttribute : Attribute
{
    readonly string _field;

    public UnitTestAttribute(string field)
    {
        _field = field;
    }
    public string Field
    {
        get { return _field; }
    }
}
[UnitTest("")]
public class PropTest1
{
    [UnitTest("123")]
    public string Prop1 { get; set; }
    [UnitTest("233")]
    public string Prop2 { get; set; }
    public string Prop3 { get; set; }
    [UnitTest("999")]
    public string Field1;
}
public class PropTest0
{
    public string Prop0 { get; set; }
    public PropTest1 Prop { get; set; }
}

public class PropTest2
{
    [UnitTest("")]
    public string Prop1 { get; set; }
    [UnitTest("")]
    public int Prop2 { get; set; }
    public bool Prop3 { get; set; }
    [UnitTest("")]
    public DateTime Filed1;
    [UnitTest("")]
    public double Filed2;
    public long Filed3;
}


public interface ITestInterface
{
    void Test();
    void Test(string input);
    void Test(ITestInterface input);
}
public interface ITestInterface<T> : ITestInterface
{
    void Test(ITestInterface<T> input);
}
public interface ITestInterface<T, TT> : ITestInterface<T>
{
    void Test(ITestInterface<T, TT> input);
}
public class InterfaceTestClass : ITestInterface
{
    public void Test()
    {
        throw new NotImplementedException();
    }

    public void Test(string input)
    {
        throw new NotImplementedException();
    }

    public void Test(ITestInterface input)
    {
        throw new NotImplementedException();
    }
}
public class InterfaceTestClass2 : InterfaceTestClass { }
public class InterfaceTestClass3 : InterfaceTestClass2, ITestInterface<PropTest1>
{
    public void Test(ITestInterface<PropTest1> input)
    {
        throw new NotImplementedException();
    }
}
public class InterfaceTestClass4<T> : ITestInterface<T>
{
    public void Test(ITestInterface<T> input)
    {
        throw new NotImplementedException();
    }

    public void Test()
    {
        throw new NotImplementedException();
    }

    public void Test(string input)
    {
        throw new NotImplementedException();
    }

    public void Test(ITestInterface input)
    {
        throw new NotImplementedException();
    }
}
public class InterfaceTestClass5 : InterfaceTestClass4<PropTest1>, ITestInterface<PropTest1, PropTest0>
{
    public void Test(ITestInterface<PropTest1, PropTest0> input)
    {
        throw new NotImplementedException();
    }
}
public abstract class AbsClass { }
public enum TestEnum { }
public class MyEnumerable : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class TypeTest
{
    [Theory]
    [InlineData(2333, 2333, 2333.2333, "2333", true)]
    public void CheckBaseTypeTest(int intValue, long longValue, double doubleValue, string stringValue, bool boolValue)
    {
        Assert.True(intValue.IsBuildInType());
        Assert.True(longValue.IsBuildInType());
        Assert.True(doubleValue.IsBuildInType());
        Assert.True(stringValue.IsBuildInType());
        Assert.True(boolValue.IsBuildInType());
        Assert.True(boolValue.IsBaseType());
    }

    [Fact]
    public void PropNamesTest()
    {
        var props = typeof(PropTest1).PropNames();
        Assert.True(props.Count() == 3 && props.AllContain("Prop1", "Prop2", "Prop3"));
        props = new PropTest1().PropNames();
        Assert.True(props.Count() == 3 && props.AllContain("Prop1", "Prop2", "Prop3"));
        props = new PropTest0().PropNames();
        Assert.True(props.Count() == 2 && props.AllContain("Prop0", "Prop"));
    }

    [Fact]
    public void PropNamesHasDepthTest()
    {
        var props = new PropTest0().PropNames(1);
        Assert.True(props.Count() == 4 && props.AllContain("Prop0", "Prop.Prop1", "Prop.Prop2", "Prop.Prop3"));
        props = typeof(PropTest0).PropNames(0);
        Assert.True(props.Count() == 2 && props.AllContain("Prop0", "Prop"));
    }
    [Fact]
    public void GetBuildInTypePropNames()
    {
        var props = new PropTest0().BuildInTypePropNames();
        Assert.True(props.Count() == 1);
        props = new PropTest1().BuildInTypePropNames();
        Assert.True(props.Count() == 3);
    }
    [Fact]
    public void GetValueTest()
    {
        var obj = new PropTest1
        {
            Prop1 = "1",
            Prop2 = "2",
            Prop3 = "3",
        };
        var data = obj.GetValue("Prop1");
        Assert.True(data.ToString() == "1");
        data = obj.GetValue("Prop2");
        Assert.True(data.ToString() == "2");
        data = obj.GetValue("Prop3");
        Assert.True(data.ToString() == "3");
        data = obj.GetValue("Prop4");
        Assert.True(data == null);
    }

    [Fact]
    public void BuildInPropsTest()
    {
        var props = typeof(PropTest1).BuildInTypePropNames();
        Assert.True(props.Count() == 3);
        props = typeof(PropTest0).BuildInTypePropNames();
        Assert.True(props.Count() == 1);
    }

    [Fact]
    public void SetValueTest()
    {
        var data = new PropTest1 { Prop1 = "1" };
        Assert.True(data.Prop1 == "1");
        data.SetValue("Prop1", "233");
        Assert.True(data.Prop1 == "233");
    }

    [Fact]
    public void BuildInTypeValueTest()
    {
        var data = new PropTest0
        {
            Prop0 = "233",
            Prop = new()
            {
                Prop1 = "1",
                Prop2 = "2",
                Prop3 = "3",
            }
        };
        var propValues = data.BuildInTypeValues();
        Assert.True(propValues.Count == 1);
        Assert.True(propValues.First().Value.ToString() == "233");
    }

    [Fact]
    public void PropsTest()
    {
        var props = typeof(PropTest1).Props();
        var data = new PropTest1();
        var props2 = data.Props();
        Assert.True(props.SequenceEqual(props2, item => item.Name.GetHashCode()));
        Assert.True(props.Select(item => item.Name).AllContain("Prop1", "Prop2", "Prop3"));
    }

    [Fact]
    public void BuildInTypeTest()
    {
        Assert.True(true.IsBuildInType());
        Assert.True(((byte)12).IsBuildInType());
        Assert.True(((sbyte)12).IsBuildInType());
        Assert.True('a'.IsBuildInType());
        Assert.True(2.33m.IsBaseType());
        Assert.True(2.33.IsBaseType());
        Assert.True(2.33f.IsBaseType());
        Assert.True(233.IsBaseType());
        Assert.True(((uint)233).IsBaseType());
        Assert.True(((short)233).IsBaseType());
        Assert.True(((ushort)233).IsBaseType());
        Assert.True(233333333333.IsBaseType());
        Assert.True(((ulong)233333333333).IsBaseType());
        Assert.True(DateTime.Now.IsBuildInType());
        Assert.True(Guid.NewGuid().IsBuildInType());
        Assert.True(typeof(nint).IsBuildInType());
        Assert.True(typeof(nuint).IsBaseType());
    }

    [Fact]
    public void SetValueWithPropsTest()
    {
        PropTest0 testData = new()
        {
            Prop0 = "string",
        };
        testData.SetValue("Prop.Prop2", "SetValue");
        Assert.True(testData.Prop.Prop2 == "SetValue");
    }

    [Fact]
    public void GetValueWithPropsTest()
    {
        PropTest0 testData = new()
        {
            Prop0 = "string",
            Prop = new()
            {
                Prop1 = "value1",
                Prop2 = "value2",
            }
        };
        Assert.True(testData.GetValue("Prop.Prop1").ToString() == "value1");
    }
    [Fact]
    public void AnonymousSetValueTest()
    {
        var data = new
        {
            Index = 0,
            Name = "",
            Item = new
            {
                I_Index = 0,
                I_Name = ""
            }
        };
        Assert.True(data.Index == 0);
        Assert.True(data.Name == "");
        data.SetAnonymousValue("Index", 233);
        Assert.True(data.Index == 233);

        data.SetAnonymousValue("Item.I_Index", 233);
        Assert.True(data.Item.I_Index == 233);
    }
    [Fact]
    public void AnonymousGetValueTest()
    {
        var data = new
        {
            Index = 233,
            Name = "233",
            Item = new
            {
                I_Index = 233,
                I_Name = "233"
            }
        };
        Assert.True(data.Index == 233);
        Assert.True(data.Name == "233");

        Assert.True((int)data.GetValue("Index") == 233);
        Assert.True((int)data.GetValue("Item.I_Index") == 233);
    }

    [Fact]
    public void ObjDifferentTest()
    {
        PropTest1 obj1 = new() { Prop1 = "1", Prop2 = "2", Prop3 = "3" };
        PropTest1 obj2 = new() { Prop1 = "1", Prop2 = "2", Prop3 = "33" };
        var difference = obj1.Difference(obj2);
        Assert.Single(difference.Diffs);
        var item = difference.GetDiff("Prop3");
        Assert.Equal("3", item.Beforevalue);
        item = difference.GetDiff(typeof(PropTest1).GetProperty("Prop3"));
        Assert.Equal("33", item.Endvalue);
    }

    [Fact]
    public void HasInterfaceTest()
    {
        Assert.True(typeof(InterfaceTestClass).HasInterface(typeof(ITestInterface)));
        Assert.True(typeof(InterfaceTestClass).HasInterface<ITestInterface>());
        Assert.True(typeof(InterfaceTestClass2).HasInterface(typeof(ITestInterface)));
        Assert.True(typeof(InterfaceTestClass3).HasInterface(typeof(ITestInterface)));
        Assert.False(typeof(InterfaceTestClass3).HasInterface(typeof(InterfaceTestClass)));
        Assert.True(typeof(InterfaceTestClass3).HasGenericInterface(typeof(ITestInterface<>)));
    }

    [Fact]
    public void HasParameterTest()
    {
        Assert.Contains(typeof(InterfaceTestClass).GetMethods(), item => item.HasParameter<string>());
        Assert.Contains(typeof(InterfaceTestClass2).GetMethods(), item => item.HasParameter(typeof(ITestInterface)));
        Assert.Contains(typeof(InterfaceTestClass3).GetMethods(), item => item.HasParameter(typeof(ITestInterface<PropTest1>)));

        Assert.Contains(typeof(InterfaceTestClass3).GetMethods(), item => item.HasGenericParamter(typeof(ITestInterface<>)));
        Assert.DoesNotContain(typeof(InterfaceTestClass3).GetMethods(), item => item.HasGenericParamter(typeof(ITestInterface)));

        Assert.Equal(2, typeof(InterfaceTestClass5).GetMethods().Count(item => item.HasGenericParamter(typeof(ITestInterface<>))));
        Assert.Equal(2, typeof(InterfaceTestClass3).GetMethods().Count(item => item.HasParameter<ITestInterface>()));
    }

}
