using System.Xml;

namespace Collapsenav.Net.Tool;

public class XmlNodeCache
{
    public XmlNodeCache(XmlDocument doc, string path)
    {
        Doc = doc;
        Path = path;
        NodeList = doc.SelectNodes(path);
        Nodes = NodeList.GetNodes();
    }
    public XmlDocument Doc { get; private set; }
    public string Path { get; private set; }
    public IEnumerable<XmlNode> Nodes { get; private set; }
    public XmlNodeList NodeList { get; set; }
}

public static class XmlExt
{
    static XmlExt()
    {
        // 获取目录中所有 xmldoc
        Docs = Directory.GetFiles(AppContext.BaseDirectory, "*.xml").Select(item => item.GetXmlDocument()).ToList();

        // 根据默认的xml注释生成规则先生成对应的summarynode
        NodeCaches = new();
        Docs.ForEach(doc =>
        {
            NodeCaches.Add(new XmlNodeCache(doc, SummaryNodePath));
        });
        SummaryDict = NodeCaches.ToDictionary(item => item.Doc, item => item.Nodes.Select(node => new SummaryNode(node)));
    }
    public const string SummaryNodePath = "doc/members/member";
    /// <summary>
    /// 基本上不太可能会在运行的过程中改变, 所以写个静态存着
    /// </summary>
    private static IEnumerable<XmlDocument> Docs;
    private static Dictionary<XmlDocument, IEnumerable<SummaryNode>> SummaryDict;
    public static List<XmlNodeCache> NodeCaches { get; private set; }
    private static void AddDocToCache(XmlDocument doc, string path)
    {
        if (!doc.In(Docs))
            Docs = Docs.Append(doc);
        if (NodeCaches.Any(item => item.Doc == doc && item.Path == path))
            return;
        var cacheNode = new XmlNodeCache(doc, path);
        NodeCaches.Add(cacheNode);
        if (path == SummaryNodePath)
            SummaryDict.Add(doc, cacheNode.Nodes.Select(node => new SummaryNode(node)));
    }
    private static void AddDocsToCache(IEnumerable<XmlDocument> docs, string path)
    {
        var notExist = docs.Except(Docs);
        if (notExist.NotEmpty())
            notExist.ForEach(item => AddDocToCache(item, path));
    }
    /// <summary>
    /// 自动搜集目录下的xml文件并转为 xmldoc
    /// </summary>
    /// <param name="reset">如果确实需要重新获取xmldoc</param>
    /// <remarks>在不设置reset的情况下会使用第一次执行时获取的缓存</remarks>
    public static IEnumerable<XmlDocument> GetXmlDocuments(bool reset = false)
    {
        if (reset)
            Docs = Directory.GetFiles(AppContext.BaseDirectory, "*.xml").Select(item => item.GetXmlDocument()).ToList();
        return Docs;
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
        // 先看有无缓存
        var cache = NodeCaches.FirstOrDefault(item => item.Doc == doc && item.Path == path);
        if (cache != null)
            return cache.NodeList;
        AddDocToCache(doc, path);
        return NodeCaches.FirstOrDefault(item => item.Doc == doc && item.Path == path).NodeList;
    }
    /// <summary>
    /// 获取nodelist
    /// </summary>
    public static IEnumerable<XmlNodeList> GetNodeLists(this IEnumerable<XmlDocument> docs, string path)
    {
        AddDocsToCache(docs, path);
        return NodeCaches.Where(item => item.Doc.In(docs)).Select(item => item.NodeList);
    }
    /// <summary>
    /// 获取xmlnode
    /// </summary>
    public static IEnumerable<XmlNode> GetNodes(this IEnumerable<XmlDocument> docs, string path)
    {
        AddDocsToCache(docs, path);
        return NodeCaches.SelectMany(item => item.Nodes);
    }
    /// <summary>
    /// 获取注释node
    /// </summary>
    public static IEnumerable<SummaryNode> GetSummaryNodes(this IEnumerable<XmlDocument> docs)
    {
        AddDocsToCache(docs, SummaryNodePath);
        return SummaryDict.Where(item => item.Key.In(docs)).SelectMany(item => item.Value);
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