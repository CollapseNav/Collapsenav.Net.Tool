using System.Xml;

namespace Collapsenav.Net.Tool;

public static class XmlExt
{
    /// <summary>
    /// 自动搜集目录下的xml文件并转为 xmldoc
    /// </summary>
    public static IEnumerable<XmlDocument> GetXmlDocuments()
    {
        return Directory.GetFiles(AppContext.BaseDirectory, "*.xml").Select(item => item.GetXmlDocument());
    }
    /// <summary>
    /// 从指定路径读取xml文件并转为 xmldoc
    /// </summary>
    public static XmlDocument GetXmlDocument(this string path)
    {
        XmlDocument doc = new();
        doc.Load(path);
        return doc;
    }
    /// <summary>
    /// 获取nodelist
    /// </summary>
    public static XmlNodeList GetNodeList(this XmlDocument doc, string path)
    {
        return doc.SelectNodes(path);
    }
    /// <summary>
    /// 获取nodelist
    /// </summary>
    public static IEnumerable<XmlNodeList> GetNodeLists(this IEnumerable<XmlDocument> docs, string path)
    {
        return docs.Select(item => item.GetNodeList(path));
    }
    /// <summary>
    /// 获取xmlnode
    /// </summary>
    public static IEnumerable<XmlNode> GetNodes(this IEnumerable<XmlDocument> docs, string path)
    {
        return docs.GetNodeLists(path).SelectMany(item => item.GetNodes());
    }
    /// <summary>
    /// 获取注释node
    /// </summary>
    public static IEnumerable<SummaryNode> GetSummaryNodes(this IEnumerable<XmlDocument> docs)
    {
        // dotnet 自动生成的注释有固定的格式
        return docs.GetNodes("doc/members/member").Select(item => new SummaryNode(item));
    }
    /// <summary>
    /// 获取xmlnode
    /// </summary>
    public static IEnumerable<XmlNode> GetNodes(this XmlNodeList nodeList)
    {
        List<XmlNode> nodes = new();
        foreach (var node in nodeList)
        {
            if (node is XmlNode xmlNode)
                nodes.Add(xmlNode);
        }
        return nodes;
    }
}