using System.Text.Json.Serialization;

namespace Collapsenav.Net.Tool.Region;

public class RegionNode
{
    public string Name { get; set; }
    public string Code { get; set; }
}
public class RegionTreeNode : RegionNode
{
    public RegionTreeNode() { }
    public IReadOnlyCollection<RegionTreeNode> Child { get; set; }
    [JsonIgnore]
    public RegionTreeNode Parent { get; set; }

    public static Task<RegionTreeNode> GetAllNodeAsync()
    {
        return LoadRegionNodeTool.LoadTreeNodeAsync();
    }

    public RegionNode ToNode()
    {
        return new RegionNode
        {
            Code = Code,
            Name = Name,
        };
    }
}