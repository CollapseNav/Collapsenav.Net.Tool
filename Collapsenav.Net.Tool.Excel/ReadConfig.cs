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
        /// <summary>
        /// 依据表头的设置
        /// </summary>
        public ICollection<ReadCellOption<T>> FieldOption { get; set; }
        /// <summary>
        /// 一些默认的初始化设置
        /// </summary>
        public ICollection<ReadCellOption<T>> DefaultOption { get; set; }
        /// <summary>
        /// 随便你怎么搞的设置
        /// </summary>
        public Func<T, T> Init { get; set; }
    }
}
