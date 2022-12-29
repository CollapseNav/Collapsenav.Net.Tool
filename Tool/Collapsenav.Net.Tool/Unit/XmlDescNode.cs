using System.Xml;

namespace Collapsenav.Net.Tool;

public class ParamNode
{
    public ParamNode(string name, string desc)
    {
        Name = name;
        Desc = desc;
    }

    public string Name { get; set; }
    public string Desc { get; set; }
}
public class SummaryNode
{
    public XmlNodeMemberTypeEnum NodeType { get; set; }
    public string Summary { get; set; }
    public string FullName { get; set; }
    public IEnumerable<ParamNode> ParamsDesc { get; set; }
    public SummaryNode(XmlNode node)
    {
        FullName = node.Attributes!["name"]!.Value;
        NodeType = FullName.First() switch
        {
            "N" => XmlNodeMemberTypeEnum.Namespace,
            "T" => XmlNodeMemberTypeEnum.Type,
            "F" => XmlNodeMemberTypeEnum.Field,
            "P" => XmlNodeMemberTypeEnum.Property,
            "M" => XmlNodeMemberTypeEnum.Method,
            "E" => XmlNodeMemberTypeEnum.Event,
            _ => XmlNodeMemberTypeEnum.UnKnow
        };
        if (node.HasChildNodes)
        {
            foreach (var child in node.ChildNodes)
            {
                if (child is XmlNode childNode)
                {
                    if (childNode.Name == "summary")
                    {
                        Summary = childNode.InnerText.Trim();
                        break;
                    }
                }
            }
            var paramsNodes = node.SelectNodes("param").GetNodes();
            if (paramsNodes.NotEmpty())
            {
                ParamsDesc = paramsNodes.Select(item => new ParamNode(item.Attributes!["name"]!.Value, item.InnerText.Trim()));
            }
        }
    }
}