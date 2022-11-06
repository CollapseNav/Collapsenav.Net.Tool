using System.Text.Json;

namespace Collapsenav.Net.Tool.Region;

internal static class LoadRegionNodeTool
{
    internal const string TreeNodeName = "region-tree.json";
    internal static RegionTreeNode LoadTreeNode()
    {
#if NETSTANDARD2_0
        var str = File.ReadAllText($"{AppContext.BaseDirectory}/{TreeNodeName}");
#else
        var str = File.ReadAllText($"{AppContext.BaseDirectory}/{TreeNodeName}");
#endif
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        var node = JsonSerializer.Deserialize<RegionTreeNode>(str, options);
        LoadParentNode(node);
        return node;
    }

    internal static void LoadParentNode(RegionTreeNode node)
    {
        foreach (var n in node.Child)
        {
            n.Parent = node;
            if (n.Child != null)
                LoadParentNode(n);
        }
    }
}