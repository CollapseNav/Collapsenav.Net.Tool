using System.Text.Json;

namespace Collapsenav.Net.Tool.Region;

public static class LoadRegionNodeTool
{
    public const string TreeNodeName = "region-tree.json";
    public static async Task<RegionTreeNode> LoadTreeNodeAsync()
    {
        var str = await File.ReadAllTextAsync($"{AppContext.BaseDirectory}/{TreeNodeName}");
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