using System.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Test
{
    public class PropTest1
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        public string Prop3 { get; set; }
        public string Function1()
        {
            return "";
        }
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
    }
}
