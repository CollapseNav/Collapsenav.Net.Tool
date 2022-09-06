using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Region;

string regionPath = "./region.json";

// var node = await LoadRegionGeneratorNodeFromJsonFileAsync(regionPath);

// await PullRegionDataAsync(node, 4);

// var treenode = node.ToTreeNode();

// treenode.ToJson().ToBytes().SaveTo("./region-tree.json");

// await SaveToTreeNodeAsync(treenode, "./region-tree");


var randNode = Region.AllCities.Shuffle().First();


Console.WriteLine($"{randNode.Province().Name}/{randNode.City().Name}");

async Task<RegionGeneratorNode> LoadRegionGeneratorNodeFromJsonFileAsync(string path)
{
    string jstring;
    jstring = await File.ReadAllTextAsync(path);
    var node = RegionTool.RootNode();

    if (jstring.NotEmpty())
        node = jstring.ToObj<RegionGeneratorNode>();
    return node;
}

async Task PullRegionDataAsync(RegionGeneratorNode node, int level)
{
    try
    {
        if (node == null)
            node = await RegionTool.GenRegionNodeAsync(level);
        else
            node = await node.GenRegionNodeAsync(level);
        await node.ToJson().ToBytes().SaveToAsync(regionPath);
    }
    catch (Exception ex)
    {
        await node.ToJson().ToBytes().SaveToAsync(regionPath);
        Console.WriteLine(ex.Message);
    }
}