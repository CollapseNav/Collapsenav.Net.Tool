namespace Collapsenav.Net.Tool.Region;

public static class Region
{
    static Region()
    {
        Tree = LoadRegionNodeTool.LoadTreeNodeAsync().Result;
        InitByProvince();
    }
    public static RegionTreeNode Tree { get; private set; }
    /// <summary>
    /// 所有省份
    /// </summary>
    public static IReadOnlyCollection<RegionNode> AllProvinces { get; private set; }
    /// <summary>
    /// 所有省份(树节点)
    /// </summary>
    public static IReadOnlyCollection<RegionTreeNode> AllTreeProvinces { get; private set; }
    /// <summary>
    /// 所有城市
    /// </summary>
    public static IReadOnlyCollection<RegionNode> AllCities { get; private set; }
    /// <summary>
    /// 所有城市(树节点)
    /// </summary>
    public static IReadOnlyCollection<RegionTreeNode> AllTreeCities { get; private set; }
    /// <summary>
    /// 尝试获取省
    /// </summary>
    public static RegionNode Province(this RegionNode node)
    {
        return node.Code.Length < 2 ? null : AllProvinces.FirstOrDefault(item => item.Code == node.Code.Substring(0, 2));
    }
    /// <summary>
    /// 尝试获取省(树节点)
    /// </summary>
    public static RegionTreeNode TreeProvince(this RegionNode node)
    {
        return node.Code.Length < 2 ? null : AllTreeProvinces.FirstOrDefault(item => item.Code == node.Code.Substring(0, 2));
    }
    /// <summary>
    /// 尝试获取市
    /// </summary>
    public static RegionNode City(this RegionNode node)
    {
        return node.Code.Length < 4 ? null : AllCities.FirstOrDefault(item => item.Code == node.Code.Substring(0, 4));
    }
    /// <summary>
    /// 尝试获取市(树节点)
    /// </summary>
    public static RegionTreeNode TreeCity(this RegionNode node)
    {
        return node.Code.Length < 4 ? null : AllTreeCities.FirstOrDefault(item => item.Code == node.Code.Substring(0, 4));
    }
    /// <summary>
    /// 获取省
    /// </summary>
    public static RegionNode GetProvince(string code)
    {
        return AllProvinces.FirstOrDefault(item => item.Code == code.Substring(0, 2));
    }
    /// <summary>
    /// 获取省(树节点)
    /// </summary>
    public static RegionTreeNode GetTreeProvince(string code)
    {
        return AllTreeProvinces.FirstOrDefault(item => item.Code == code.Substring(0, 2));
    }
    /// <summary>
    /// 获取市
    /// </summary>
    public static RegionNode GetCity(string code)
    {
        return GetTreeProvince(code)?.Child.FirstOrDefault(item => item.Code == code.Substring(0, 4))?.ToNode();
    }
    /// <summary>
    /// 获取市(树节点)
    /// </summary>
    public static RegionTreeNode GetTreeCity(string code)
    {
        return GetTreeProvince(code)?.Child.FirstOrDefault(item => item.Code == code.Substring(0, 4)); ;
    }

    /// <summary>
    /// 根据传入的省 code 初始化
    /// </summary>
    /// <remarks>如果传入了32, 则 AllProvinces 和 AllCities 只会保存江苏的 省,市</remarks>
    public static void InitByProvince(params string[] provinces)
    {
        if (provinces == null || provinces.Length == 0)
        {
            AllTreeProvinces = Tree.Child.ToList();
            AllProvinces = AllTreeProvinces.Select(item => item.ToNode()).ToList();
            AllTreeCities = AllTreeProvinces.SelectMany(item => item.Child).ToList();
            AllCities = AllTreeCities.Select(item => item.ToNode()).ToList();
        }
        else
        {
            AllTreeProvinces = Tree.Child.Where(item => provinces.Contains(item.Code)).ToList();
            AllProvinces = AllTreeProvinces.Select(item => item.ToNode()).ToList();
            AllTreeCities = AllTreeProvinces.SelectMany(item => item.Child).ToList();
            AllCities = AllTreeCities.Select(item => item.ToNode()).ToList();
        }
    }
}
