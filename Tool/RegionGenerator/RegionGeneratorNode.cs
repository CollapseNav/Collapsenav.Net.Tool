namespace Collapsenav.Net.Tool.Region;
public class RegionGeneratorNode : RegionNode
{
    public RegionGeneratorNode() { }
    public RegionGeneratorNode(RegionGeneratorNode parent)
    {
        ParentNode = parent;
    }

    public string ParentCode { get => ParentNode?.Code; }
    public RegionGeneratorNode ParentNode { get; set; }
    public string Url
    {
        get
        {
            if (!string.IsNullOrEmpty(url) && url.StartsWith("http"))
                return url;
            if (ParentNode == null)
                return $"{RegionTool.DataSourceUrl}/{url}";
            string parentUrl = ParentNode.Url;
            if (parentUrl.EndsWith(".html"))
            {
                return $"{parentUrl.Substring(0, parentUrl.LastIndexOf("/"))}{(string.IsNullOrEmpty(ParentCode) ? "" : $"/{ParentCode.Substring(ParentCode.Length - 2)}")}/{url}";
            }
            return "";
        }
        set => url = $"{value}";
    }
    private string url;
    public IEnumerable<RegionGeneratorNode> ChildNode { get; set; }
    public int? ChildNum { get; set; }


    public RegionTreeNode ToTreeNode(RegionTreeNode parent = null)
    {
        RegionTreeNode node = new()
        {
            Name = Name,
            Code = Code,
        };
        node.Child = ChildNode?.Select(item => item.ToTreeNode(node)).ToList();
        node.Parent = parent;
        return node;
    }
}