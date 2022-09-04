using System.Collections.ObjectModel;

namespace Collapsenav.Net.Tool.Region;
[Serializable]
public class RegionTreeNode
{
    public string Name { get; set; }
    public string Code { get; set; }
    public IReadOnlyCollection<RegionTreeNode> Child { get; set; }
}