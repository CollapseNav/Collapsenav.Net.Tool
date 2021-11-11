using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool.Excel
{
    public partial class ExportConfig<T>
    {
        /// <summary>
        /// 列表数据导出到excel
        /// </summary>
        public async Task<Stream> EPPlusExportAsync(string filePath, IEnumerable<T> data = null, ExportType exportType = ExportType.All)
        {
            return await EPPlusExportTool.ExportAsync(filePath, this, data, exportType);
        }
        /// <summary>
        /// 列表数据导出到excel
        /// </summary>
        public async Task<Stream> EPPlusExportAsync(Stream stream, IEnumerable<T> data = null, ExportType exportType = ExportType.All)
        {
            return await EPPlusExportTool.ExportAsync(stream, this, data, exportType);
        }
        /// <summary>
        /// 列表数据导出到excel
        /// </summary>
        public async Task<Stream> EPPlusExportHeaderAsync(string filePath)
        {
            return await EPPlusExportTool.ExportHeaderAsync(filePath, this);
        }
        /// <summary>
        /// 列表数据导出到excel
        /// </summary>
        public async Task<Stream> EPPlusExportHeaderAsync(Stream stream)
        {
            return await EPPlusExportTool.ExportHeaderAsync(stream, this);
        }
        /// <summary>
        /// 列表数据导出到excel
        /// </summary>
        public async Task<Stream> EPPlusExportDataAsync(string filePath, IEnumerable<T> data = null)
        {
            return await EPPlusExportTool.ExportDataAsync(filePath, this, data);
        }
        /// <summary>
        /// 列表数据导出到excel
        /// </summary>
        public async Task<Stream> EPPlusExportDataAsync(Stream stream, IEnumerable<T> data = null)
        {
            return await EPPlusExportTool.ExportDataAsync(stream, this, data);
        }

    }
}