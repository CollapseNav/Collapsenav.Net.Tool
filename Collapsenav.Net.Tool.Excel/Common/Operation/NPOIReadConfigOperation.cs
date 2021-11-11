using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel
{
    public partial class ReadConfig<T>
    {
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>
        public IEnumerable<string> GetNPOIExcelHeader()
        {
            if (ExcelStream == null)
                throw new Exception();
            return NPOIExcelReadTool.GetExcelHeader(ExcelStream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>x
        public IEnumerable<string> GetNPOIExcelHeader(string filepath)
        {
            return NPOIExcelReadTool.GetExcelHeader(filepath);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>
        public IEnumerable<string> GetNPOIExcelHeader(Stream stream)
        {
            return NPOIExcelReadTool.GetExcelHeader(stream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>
        public IEnumerable<string> GetNPOIExcelHeader(IWorkbook workbook)
        {
            return NPOIExcelReadTool.GetExcelHeader(workbook);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>
        public IEnumerable<string> GetNPOIExcelHeader(ISheet sheet)
        {
            return NPOIExcelReadTool.GetExcelHeader(sheet);
        }



        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetNPOIExcelDataAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await NPOIExcelReadTool.GetExcelDataAsync(new NPOINotCloseStream(ExcelStream).GetWorkBook());
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetNPOIExcelDataAsync(string filepath)
        {
            return await NPOIExcelReadTool.GetExcelDataAsync(filepath);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetNPOIExcelDataAsync(Stream stream)
        {
            return await NPOIExcelReadTool.GetExcelDataAsync(stream);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetNPOIExcelDataAsync(IWorkbook workbook)
        {
            return await NPOIExcelReadTool.GetExcelDataAsync(workbook);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetNPOIExcelDataAsync(ISheet sheet)
        {
            return await NPOIExcelReadTool.GetExcelDataAsync(sheet);
        }


        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> GetNPOIExcelHeaderByOptions()
        {
            if (ExcelStream == null)
                throw new Exception();
            return NPOIExcelReadTool.GetExcelHeaderByOptions(new NPOINotCloseStream(ExcelStream).GetWorkBook(), this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> GetNPOIExcelHeaderByOptions(string filepath)
        {
            return NPOIExcelReadTool.GetExcelHeaderByOptions(filepath, this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> GetNPOIExcelHeaderByOptions(Stream stream)
        {
            return NPOIExcelReadTool.GetExcelHeaderByOptions(stream, this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> GetNPOIExcelHeaderByOptions(IWorkbook workbook)
        {
            return NPOIExcelReadTool.GetExcelHeaderByOptions(workbook, this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> GetNPOIExcelHeaderByOptions(ISheet sheet)
        {
            return NPOIExcelReadTool.GetExcelHeaderByOptions(sheet, this);
        }



        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> GetNPOIExcelDataByOptionsAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await NPOIExcelReadTool.GetExcelDataByOptionsAsync(new NPOINotCloseStream(ExcelStream).GetWorkBook(), this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> GetNPOIExcelDataByOptionsAsync(string filepath)
        {
            return await NPOIExcelReadTool.GetExcelDataByOptionsAsync(filepath, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> GetNPOIExcelDataByOptionsAsync(Stream stream)
        {
            return await NPOIExcelReadTool.GetExcelDataByOptionsAsync(stream, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> GetNPOIExcelDataByOptionsAsync(IWorkbook workbook)
        {
            return await NPOIExcelReadTool.GetExcelDataByOptionsAsync(workbook, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> GetNPOIExcelDataByOptionsAsync(ISheet sheet)
        {
            return await NPOIExcelReadTool.GetExcelDataByOptionsAsync(sheet, this);
        }




        /// <summary>
        /// 将表格数据转换为指定的数据实体-NPOI
        /// </summary>
        public async Task<IEnumerable<T>> NPOIExcelToEntityAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await NPOIExcelReadTool.ExcelToEntityAsync(new NPOINotCloseStream(ExcelStream).GetWorkBook(), this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-NPOI
        /// </summary>
        public async Task<IEnumerable<T>> NPOIExcelToEntityAsync(string filepath)
        {
            return await NPOIExcelReadTool.ExcelToEntityAsync(filepath, this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-NPOI
        /// </summary>
        public async Task<IEnumerable<T>> NPOIExcelToEntityAsync(Stream stream)
        {
            return await NPOIExcelReadTool.ExcelToEntityAsync(stream, this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-NPOI
        /// </summary>
        public async Task<IEnumerable<T>> NPOIExcelToEntityAsync(IWorkbook workbook)
        {
            return await NPOIExcelReadTool.ExcelToEntityAsync(workbook, this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-NPOI
        /// </summary>
        public async Task<IEnumerable<T>> NPOIExcelToEntityAsync(ISheet sheet)
        {
            return await NPOIExcelReadTool.ExcelToEntityAsync(sheet, this);
        }

    }
}