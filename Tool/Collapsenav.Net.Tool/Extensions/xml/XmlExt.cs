using System.Xml;

namespace Collapsenav.Net.Tool;

public static class XmlExt
{
    public static IEnumerable<XmlDocument> GetXmlDocuments()
    {
        return Directory.GetFiles(AppContext.BaseDirectory, "*.xml").Select(item => item.GetXmlDocument());
    }
    public static XmlDocument GetXmlDocument(this string path)
    {
        XmlDocument doc = new();
        doc.Load(path);
        return doc;
    }

    public static XmlNodeList GetNodeList(this XmlDocument doc, string path)
    {
        return doc.SelectNodes(path);
    }

    public static IEnumerable<XmlNodeList> GetNodeLists(this IEnumerable<XmlDocument> docs, string path)
    {
        return docs.Select(item => item.GetNodeList(path));
    }

    public static IEnumerable<XmlNode> GetNodes(this IEnumerable<XmlDocument> docs, string path)
    {
        return docs.GetNodeLists(path).SelectMany(item => item.GetNodes());
    }

    public static IEnumerable<XmlNode> GetNodes(this XmlNodeList nodeList)
    {
        List<XmlNode> nodes = new();
        foreach (var node in nodeList)
        {
            if (node is XmlNode xmlNode)
            {
                nodes.Add(xmlNode);
            }
        }
        return nodes;
    }
}