using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class EnumTest
{
    enum TestEnum
    {
        A = 1,
    }
    [Fact]
    public void DescriptionTest()
    {
        var e = TestEnum.A;
        Assert.True(e.Description() == "A");
    }
}