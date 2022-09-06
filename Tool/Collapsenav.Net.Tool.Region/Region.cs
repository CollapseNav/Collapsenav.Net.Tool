namespace Collapsenav.Net.Tool.Region;

public static class Region
{
    static Region()
    {
        Tree = LoadRegionNodeTool.LoadTreeNodeAsync().Result;
        AllProvinces = Tree.Child.Select(item => item.ToNode()).ToList();
        AllCities = Tree.Child.SelectMany(item => item.Child.Select(c => c.ToNode())).ToList();
    }
    public static RegionTreeNode Tree { get; set; }
    /// <summary>
    /// 所有省份
    /// </summary>
    public static IReadOnlyCollection<RegionNode> AllProvinces { get; set; }
    /// <summary>
    /// 所有城市
    /// </summary>
    public static IReadOnlyCollection<RegionNode> AllCities { get; set; }

    public static RegionNode Province(this RegionNode node)
    {
        return node.Code.Length < 2 ? null : AllProvinces.FirstOrDefault(item => item.Code == node.Code.Substring(0, 2));
    }

    public static RegionNode City(this RegionNode node)
    {
        return node.Code.Length < 4 ? null : AllCities.FirstOrDefault(item => item.Code == node.Code.Substring(0, 4));
    }
}