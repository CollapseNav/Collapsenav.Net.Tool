using System.Linq;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class UnitTestAttribute : System.Attribute
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
    public class PropTest1
    {
        [UnitTest("123")]
        public string Prop1 { get; set; }
        [UnitTest("233")]
        public string Prop2 { get; set; }
        public string Prop3 { get; set; }
    }
    public class PropTest0
    {
        public string Prop0 { get; set; }
        public PropTest1 Prop { get; set; }
    }
    public class TypeTest
    {
        [Fact]
        public void CheckBaseTypeTest()
        {
            int intValue = 2333;
            long longValue = 2333;
            double doubleValue = 2333.2333;
            string stringValue = "23333";
            bool boolValue = true;

            Assert.True(intValue.IsBuildInType());
            Assert.True(longValue.IsBuildInType());
            Assert.True(doubleValue.IsBuildInType());
            Assert.True(stringValue.IsBuildInType());
            Assert.True(boolValue.IsBuildInType());
            Assert.True(boolValue.IsBaseType());
            Assert.False(TypeTool.IsBuildInType<TypeTest>());
            Assert.False(TypeTool.IsBaseType<TypeTest>());
            Assert.False(typeof(TypeTool).IsBaseType());
        }

        [Fact]
        public void PropNamesTest()
        {
            var props = typeof(PropTest1).PropNames();
            Assert.True(props.Count() == 3 && props.ContainAnd("Prop1", "Prop2", "Prop3"));
            props = new PropTest1().PropNames();
            Assert.True(props.Count() == 3 && props.ContainAnd("Prop1", "Prop2", "Prop3"));
            props = new PropTest0().PropNames();
            Assert.True(props.Count() == 2 && props.ContainAnd("Prop0", "Prop"));
        }

        [Fact]
        public void PropNamesHasDepthTest()
        {
            var props = new PropTest0().PropNames(1);
            Assert.True(props.Count() == 4 && props.ContainAnd("Prop0", "Prop.Prop1", "Prop.Prop2", "Prop.Prop3"));
            props = typeof(PropTest0).PropNames(0);
            Assert.True(props.Count() == 2 && props.ContainAnd("Prop0", "Prop"));
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
        public void GetAttrTest()
        {
            var attrValues = typeof(PropTest1).AttrValues<UnitTestAttribute>();
            Assert.True(attrValues.Count == 2);
            Assert.True(attrValues.First().Value.Field == "123");
            Assert.True(attrValues.Last().Value.Field == "233");
        }

        [Fact]
        public void SetValueTest()
        {
            var data = new PropTest1
            {
                Prop1 = "1"
            };
            Assert.True(data.Prop1 == "1");
            data.SetValue("Prop1", "233");
            Assert.True(data.Prop1 == "233");
        }
}
