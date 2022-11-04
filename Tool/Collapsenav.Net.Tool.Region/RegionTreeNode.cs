using System.Collections.ObjectModel;

namespace Collapsenav.Net.Tool.Region;

/// <summary>
/// 基础节点
/// </summary>
public class RegionNode
{
    public string Name { get; set; }
    public string Code { get; set; }
}
/// <summary>
/// 树形节点
/// </summary>
public class RegionTreeNode : RegionNode
{
    public RegionTreeNode() { }
    public IReadOnlyCollection<RegionTreeNode> Child { get; set; }
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

    public RegionTreeNode ToTreeNodeWithoutParent()
    {
        return new RegionTreeNode
        {
            Code = Code,
            Name = Name,
            Child = (Child == null || !Child.Any()) ? null : new ReadOnlyCollection<RegionTreeNode>(Child.Select(item => item.ToTreeNodeWithoutParent()).ToList())
        };
    }
}