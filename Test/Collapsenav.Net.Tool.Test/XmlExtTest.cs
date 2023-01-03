using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class XmlExtTest
{
    [Fact]
    public void GetXmlDocumentTest()
    {
        var docs = XmlExt.GetXmlDocuments();
        Assert.True(docs.Count() == 1);
        var nodes = docs.GetSummaryNodes();
        var node = nodes.FirstOrDefault(item => item.Summary == "注释node");
        Assert.NotNull(node);
        Assert.True(node.NodeType == XmlNodeMemberTypeEnum.Type);
    }
}