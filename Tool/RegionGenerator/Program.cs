using System.Runtime.Serialization.Formatters.Binary;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Region;

string regionPath = "./region.json";
var jstring = string.Empty;
var fs = regionPath.OpenCreateReadWriteShareStream();
if (fs.Length > 100)
    jstring = fs.ToBytes().BytesToString();
fs.Dispose();

var node = RegionTool.RootNode();

if (jstring.NotEmpty())
    node = jstring.ToObj<RegionGeneratorNode>();

var level = 4;
try
{
    if (node == null)
        node = await RegionTool.GenRegionNodeAsync(level);
    else
        node = await node.GenRegionNodeAsync(level);
    jstring = node.ToJson();
    await jstring.ToBytes().SaveToAsync(regionPath);
}
catch (Exception ex)
{
    jstring = node.ToJson();
    await jstring.ToBytes().SaveToAsync(regionPath);
    Console.WriteLine(ex.Message);
}


// var treenode = node.ToTreeNode();

using var fss = "./region".OpenCreateReadWriteShareStream();



BinaryFormatter formatter = new();
var treenode = formatter.Deserialize(fss) as RegionTreeNode;
var treeString = treenode.ToJson();
treeString.ToBytes().SaveTo("./region-tree.json");
// formatter.Serialize(fss, treenode);
Console.WriteLine();

