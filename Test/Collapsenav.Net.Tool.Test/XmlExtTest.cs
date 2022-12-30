using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class XmlExtTest
{
    [Fact]
    public void GetXmlDocumentTest()
    {
        var docs = XmlExt.GetXmlDocuments();
        Assert.True(docs.Count() == 1);
    }
}