using System.Xml;

namespace Collapsenav.Net.Tool;

/// <summary>
/// 参数node
/// </summary>
public class ParamNode
{
    public ParamNode(string name, string desc)
    {
        Name = name;
        Desc = desc;
    }
    /// <summary>
    /// 参数名
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 注释描述
    /// </summary>
    public string Desc { get; set; }
}
/// <summary>
/// 注释node
/// </summary>
public class SummaryNode
{
    /// <summary>
    /// 注释类型
    /// </summary>
    public XmlNodeMemberTypeEnum NodeType { get; set; }
    /// <summary>
    /// 注释内容
    /// </summary>
    public string Summary { get; set; }
    /// <summary>
    /// 元素完整名称
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// 参数集合
    /// </summary>
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
                if (child is XmlNode childNode && childNode.Name == "summary")
                {
                    Summary = childNode.InnerText.Trim();
                    break;
                }
            }
            var paramsNodes = node.SelectNodes("param").GetNodes();
            if (paramsNodes.NotEmpty())
                ParamsDesc = paramsNodes.Select(item => new ParamNode(item.Attributes!["name"]!.Value, item.InnerText.Trim()));
        }
    }
}