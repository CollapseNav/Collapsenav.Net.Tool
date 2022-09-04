namespace Collapsenav.Net.Tool.Region;



public static class RegionTool
{
    public static string[] Levels = new[] { "provincetr", "citytr", "countytr", "towntr", "villagetr" };

    public static string DataSourceUrl => "http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2021";
    public static RegionGeneratorNode RootNode()
    {
        return new RegionGeneratorNode
        {
            Name = "",
            Url = DataSourceUrl + "/index.html"
        };
    }
    public static async Task<RegionGeneratorNode> GenRegionNodeAsync(int level = 5)
    {
        var node = RootNode();
        await node.GenRegion(Levels.Take(level).AsEnumerable());
        return node;
    }
    public static async Task<RegionGeneratorNode> GenRegionNodeAsync(this RegionGeneratorNode node, int level = 5)
    {
        await node.GenRegion(Levels.Take(level).AsEnumerable());
        return node;
    }
}