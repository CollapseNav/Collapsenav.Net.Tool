using System;
using System.IO;
using System.Linq;

namespace Collapsenav.Net.Tool.Excel
{
    public class ExcelTool
    {
        /// <summary>
        /// 是否 xls 文件
        /// </summary>
        public static bool IsXls(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException($@"我找不到这个叫 {filepath} 的文件,你看看是不是路径写错了");
            var ext = Path.GetExtension(filepath).ToLower();
            if (!new[] { ".xlsx", ".xls" }.Contains(ext))
                throw new Exception("文件必须为excel格式");
            return ext == ".xls";
        }
    }
}
