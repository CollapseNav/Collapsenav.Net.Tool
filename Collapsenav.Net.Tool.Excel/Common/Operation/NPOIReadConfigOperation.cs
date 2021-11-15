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
        public IEnumerable<string> NPOIExcelHeader()
        {
            if (ExcelStream == null)
                throw new Exception();
            return NPOIExcelReadTool.ExcelHeader(ExcelStream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>x
        public IEnumerable<string> NPOIExcelHeader(string filepath)
        {
            return NPOIExcelReadTool.ExcelHeader(filepath);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>
        public IEnumerable<string> NPOIExcelHeader(Stream stream)
        {
            return NPOIExcelReadTool.ExcelHeader(stream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>
        public IEnumerable<string> NPOIExcelHeader(IWorkbook workbook)
        {
            return NPOIExcelReadTool.ExcelHeader(workbook);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-NPOI
        /// </summary>
        public IEnumerable<string> NPOIExcelHeader(ISheet sheet)
        {
            return NPOIExcelReadTool.ExcelHeader(sheet);
        }



        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> NPOIExcelDataAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await NPOIExcelReadTool.ExcelDataAsync(new NPOINotCloseStream(ExcelStream).GetWorkBook());
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> NPOIExcelDataAsync(string filepath)
        {
            return await NPOIExcelReadTool.ExcelDataAsync(filepath);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> NPOIExcelDataAsync(Stream stream)
        {
            return await NPOIExcelReadTool.ExcelDataAsync(stream);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> NPOIExcelDataAsync(IWorkbook workbook)
        {
            return await NPOIExcelReadTool.ExcelDataAsync(workbook);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-NPOI
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> NPOIExcelDataAsync(ISheet sheet)
        {
            return await NPOIExcelReadTool.ExcelDataAsync(sheet);
        }


        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> NPOIExcelHeaderByOptions()
        {
            if (ExcelStream == null)
                throw new Exception();
            return NPOIExcelReadTool.ExcelHeaderByOptions(new NPOINotCloseStream(ExcelStream).GetWorkBook(), this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> NPOIExcelHeaderByOptions(string filepath)
        {
            return NPOIExcelReadTool.ExcelHeaderByOptions(filepath, this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> NPOIExcelHeaderByOptions(Stream stream)
        {
            return NPOIExcelReadTool.ExcelHeaderByOptions(stream, this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> NPOIExcelHeaderByOptions(IWorkbook workbook)
        {
            return NPOIExcelReadTool.ExcelHeaderByOptions(workbook, this);
        }
        /// <summary>
        /// 根据传入配置 获取表头及其index-NPOI
        /// </summary>
        public Dictionary<string, int> NPOIExcelHeaderByOptions(ISheet sheet)
        {
            return NPOIExcelReadTool.ExcelHeaderByOptions(sheet, this);
        }



        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> NPOIExcelDataByOptionsAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await NPOIExcelReadTool.ExcelDataByOptionsAsync(new NPOINotCloseStream(ExcelStream).GetWorkBook(), this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> NPOIExcelDataByOptionsAsync(string filepath)
        {
            return await NPOIExcelReadTool.ExcelDataByOptionsAsync(filepath, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> NPOIExcelDataByOptionsAsync(Stream stream)
        {
            return await NPOIExcelReadTool.ExcelDataByOptionsAsync(stream, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> NPOIExcelDataByOptionsAsync(IWorkbook workbook)
        {
            return await NPOIExcelReadTool.ExcelDataByOptionsAsync(workbook, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-NPOI
        /// </summary>
        public async Task<string[][]> NPOIExcelDataByOptionsAsync(ISheet sheet)
        {
            return await NPOIExcelReadTool.ExcelDataByOptionsAsync(sheet, this);
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