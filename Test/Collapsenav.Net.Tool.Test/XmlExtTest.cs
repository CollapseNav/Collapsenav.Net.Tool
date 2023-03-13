using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class XmlExtTest
{
    [Fact]
    public void GetXmlDocumentTest()
    {
        var docs = XmlExt.GetXmlDocuments();
        Assert.True(docs.Count() == 2);
        var nodes = docs.GetSummaryNodes();
        var node = nodes.FirstOrDefault(item => item.Summary == "注释node");
        Assert.NotNull(node);
        Assert.True(node.NodeType == XmlNodeMemberTypeEnum.Type);
    }

    [Fact]
    public void GetXmlNodeListTest()
    {
        var docs = XmlExt.GetXmlDocuments();
        var nodeLists = docs.GetNodeLists(XmlExt.SummaryNodePath);
        Assert.True(nodeLists.Count() == 2);
    }

    [Fact]
    public void XmlCacheTest()
    {
        var cache = XmlExt.NodeCaches;
        Assert.True(cache.Count == 2);
        Assert.True(cache.All(item => item.Path == XmlExt.SummaryNodePath));
    }
}