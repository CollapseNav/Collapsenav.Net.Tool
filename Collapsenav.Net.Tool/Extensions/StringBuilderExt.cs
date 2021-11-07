using System.Text;

namespace Collapsenav.Net.Tool
{
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
        public static StringBuilder AndIf(this StringBuilder self, bool flag, string data)
        {
            if (flag)
                self.Append($" AND {data}");
            return self;
        }
        public static StringBuilder AndIf(this StringBuilder self, string flag, string data = null)
        {
            if (data.IsNull())
                data = flag;
            if (flag.NotEmpty())
                self.Append($" AND {data}");
            return self;
        }

        public static StringBuilder OrIf(this StringBuilder self, bool flag, string data)
        {
            if (flag)
                self.Append($" OR {data}");
            return self;
        }
        public static StringBuilder OrIf(this StringBuilder self, string flag, string data = null)
        {
            if (data.IsNull())
                data = flag;
            if (flag.NotEmpty())
                self.Append($" OR {data}");
            return self;
        }
    }
}
