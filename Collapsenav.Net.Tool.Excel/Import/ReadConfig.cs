using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel
{
    public class ReadConfig<T>
    {
        private Stream ExcelStream;
        public ReadConfig(bool createSheet = false)
        {
            FieldOption = new List<ReadCellOption<T>>();
            Init = null;

            if (createSheet)
            {
            }
        }
        /// <summary>
        /// 依据表头的设置
        /// </summary>
        public IEnumerable<ReadCellOption<T>> FieldOption { get; set; }
        /// <summary>
        /// 随便你怎么搞的设置
        /// </summary>
        public Func<T, T> Init { get; set; }

        public void SetExcelStream(Stream stream)
        {
            ExcelStream = stream;
            using ExcelPackage pack = new(ExcelStream);
        }

    }
}
