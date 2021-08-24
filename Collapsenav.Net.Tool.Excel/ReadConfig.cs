using System;
using System.Collections.Generic;

namespace Collapsenav.Net.Tool.Excel
{
    public class ReadConfig<T>
    {
        public ReadConfig()
        {
            FieldOption = new List<ReadCellOption<T>>();
            DefaultOption = new List<ReadCellOption<T>>();
            Init = null;
        }
        // 依据表头的设置
        public ICollection<ReadCellOption<T>> FieldOption { get; set; }
        // 一些默认的初始化设置
        public ICollection<ReadCellOption<T>> DefaultOption { get; set; }
        // 随便你怎么搞的设置
        public Func<T, T> Init { get; set; }
    }
}
