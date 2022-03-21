using System.Text;

namespace Collapsenav.Net.Tool;
public static partial class StringBuilderExt
{
    public static StringBuilder AddIf(this StringBuilder self, bool flag, string data)
    {
        if (flag)
            self.Append(data);
        return self;
    }
    public static StringBuilder AddIf(this StringBuilder self, string flag, string data = null)
    {
        if (data.IsNull())
            data = flag;
        if (!flag.IsNull())
            self.Append(data);
        return self;
    }
}
