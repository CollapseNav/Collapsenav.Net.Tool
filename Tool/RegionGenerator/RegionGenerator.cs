using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool.Region;

/// <summary>
/// 瞎做的
/// </summary>
public static class RegionGenerator
{
    private static HttpClient client = new();
    private static readonly Random rand = new();

    internal static string ReplaceString(string input, IEnumerable<string> levels)
    {
        input = input.ToLower().Replace(" ", "");
        int index = -1;
        string level = string.Empty;
        foreach (var l in levels)
        {
            level = l;
            index = input.IndexOf($@"<trclass=""{level}"">");
            if (index < 0)
                continue;
            else if (index > 0)
                break;
        }
        if (index < 0)
            return "";
        var tempStr = input.Substring(index, input.IndexOf("</tbody>", index) - index)
        .Replace(@$"<trclass=""{level}"">", "")
        .Replace(@"<td><ahref=""", ">")
        .Replace("<br/></a></td>", "<")
        .Replace(@""">", "|");
        return tempStr;
    }
    internal static string ReplaceString(string input, string level)
    {
        input = input.ToLower().Replace(" ", "");
        var index = input.IndexOf($@"<trclass=""{level}"">");
        var tempStr = input.Substring(index, input.IndexOf("</tbody>", index) - index)
        .Replace(@$"<trclass=""{level}"">", "")
        .Replace(@"<td><ahref=""", ">")
        .Replace("<br/></a></td>", "<")
        .Replace(@""">", "|");
        return tempStr;
    }
    internal static IEnumerable<string[]> RegexMatch(string input, string pattern = @"\d+?\.html\|.+?\<{1}?")
    {
        return Regex.Matches(input, pattern).ToList()
        .Select(item => item.Value.Replace("<", "")
        .Split('|'))
        .GroupBy(item => item[0])
        .Select(item => new[] { item.Key, item.First()[1], item.Last()[1] })
        .ToList();
    }
    public static async Task<bool> GenRegion(this RegionGeneratorNode node, IEnumerable<string> levels)
    {
        if (!levels.Any())
            return true;
        if (node.ChildNode != null && node.ChildNode.Count() == node.ChildNum)
        {
            foreach (var temp in node.ChildNode)
            {
                var flag = await temp.GenRegion(levels.Skip(1).ToList());
                if (!flag)
                    break;
            }
        }
        else
        {
            if (client.Timeout != TimeSpan.FromSeconds(10))
                client.Timeout = TimeSpan.FromSeconds(10);
            var time = rand.Next(500);
            await Task.Delay(time);
            var res = await client.GetAsync(node.Url);
            var tempStr = ReplaceString(await res.Content.ReadAsStringAsync(), levels);
            if (string.IsNullOrEmpty(tempStr))
                return false;
            IEnumerable<string[]> matches = RegexMatch(tempStr);
            List<RegionGeneratorNode> nodes = new();
            node.ChildNum = matches.Count();
            foreach (var match in matches)
            {
                var temp = new RegionGeneratorNode(node)
                {
                    Name = match[2],
                    Code = match[0].Replace(".html", ""),
                    Url = match[0]
                };
                var flag = await temp.GenRegion(levels.Skip(1).ToList());
                nodes.Add(temp);
                if (!flag)
                    break;
            }
            node.ChildNode = nodes;
        }
        return true;
    }
}