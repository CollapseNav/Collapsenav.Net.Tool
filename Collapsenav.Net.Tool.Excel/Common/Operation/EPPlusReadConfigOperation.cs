using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel
{
    public partial class ReadConfig<T>
    {
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> EPPlusExcelHeader()
        {
            if (ExcelStream == null)
                throw new Exception();
            return EPPlusExcelReadTool.ExcelHeader(ExcelStream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> EPPlusExcelHeader(string filepath)
        {
            return EPPlusExcelReadTool.ExcelHeader(filepath);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> EPPlusExcelHeader(Stream stream)
        {
            return EPPlusExcelReadTool.ExcelHeader(stream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> EPPlusExcelHeader(ExcelPackage pack)
        {
            return EPPlusExcelReadTool.ExcelHeader(pack);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> EPPlusExcelHeader(ExcelWorksheet sheet)
        {
            return EPPlusExcelReadTool.ExcelHeader(sheet);
        }



        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> EPPlusExcelDataAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await EPPlusExcelReadTool.ExcelDataAsync(ExcelStream);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> EPPlusExcelDataAsync(string filepath)
        {
            return await EPPlusExcelReadTool.ExcelDataAsync(filepath);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> EPPlusExcelDataAsync(Stream stream)
        {
            return await EPPlusExcelReadTool.ExcelDataAsync(stream);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> EPPlusExcelDataAsync(ExcelPackage pack)
        {
            return await EPPlusExcelReadTool.ExcelDataAsync(pack);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> EPPlusExcelDataAsync(ExcelWorksheet sheet)
        {
            return await EPPlusExcelReadTool.ExcelDataAsync(sheet);
        }



        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> EPPlusExcelHeaderByOptions()
        {
            if (ExcelStream == null)
                throw new Exception();
            return EPPlusExcelReadTool.ExcelHeaderByOptions(ExcelStream, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> EPPlusExcelHeaderByOptions(string filepath)
        {
            return EPPlusExcelReadTool.ExcelHeaderByOptions(filepath, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> EPPlusExcelHeaderByOptions(Stream stream)
        {
            return EPPlusExcelReadTool.ExcelHeaderByOptions(stream, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> EPPlusExcelHeaderByOptions(ExcelPackage pack)
        {
            return EPPlusExcelReadTool.ExcelHeaderByOptions(pack, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> EPPlusExcelHeaderByOptions(ExcelWorksheet sheet)
        {
            return EPPlusExcelReadTool.ExcelHeaderByOptions(sheet, this);
        }



        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> EPPlusExcelDataByOptionsAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await EPPlusExcelReadTool.ExcelDataByOptionsAsync(ExcelStream, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> EPPlusExcelDataByOptionsAsync(string filepath)
        {
            return await EPPlusExcelReadTool.ExcelDataByOptionsAsync(filepath, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> EPPlusExcelDataByOptionsAsync(Stream stream)
        {
            return await EPPlusExcelReadTool.ExcelDataByOptionsAsync(stream, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> EPPlusExcelDataByOptionsAsync(ExcelPackage pack)
        {
            return await EPPlusExcelReadTool.ExcelDataByOptionsAsync(pack, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> EPPlusExcelDataByOptionsAsync(ExcelWorksheet sheet)
        {
            return await EPPlusExcelReadTool.ExcelDataByOptionsAsync(sheet, this);
        }


        /// <summary>
        /// 将表格数据转换为指定的数据实体-EPPlus
        /// </summary>
        public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await EPPlusExcelReadTool.ExcelToEntityAsync(ExcelStream, this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-EPPlus
        /// </summary>
        public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync(string filepath)
        {
            return await EPPlusExcelReadTool.ExcelToEntityAsync(filepath, this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-EPPlus
        /// </summary>
        public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync(Stream stream)
        {
            return await EPPlusExcelReadTool.ExcelToEntityAsync(stream, this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-EPPlus
        /// </summary>
        public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync(ExcelPackage pack)
        {
            return await EPPlusExcelReadTool.ExcelToEntityAsync(pack, this);
        }
        /// <summary>
        /// 将表格数据转换为指定的数据实体-EPPlus
        /// </summary>
        public async Task<IEnumerable<T>> EPPlusExcelToEntityAsync(ExcelWorksheet sheet)
        {
            return await EPPlusExcelReadTool.ExcelToEntityAsync(sheet, this);
        }
    }
}