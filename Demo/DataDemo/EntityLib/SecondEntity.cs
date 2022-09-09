using Collapsenav.Net.Tool.Data;

namespace DataDemo.EntityLib;

public class SecondEntity : BaseEntity<long?>
{
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
}