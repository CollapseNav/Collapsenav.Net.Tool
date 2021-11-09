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
        public IEnumerable<string> GetEPPlusExcelHeader()
        {
            if (ExcelStream == null)
                throw new Exception();
            return EPPlusExcelReadTool.GetExcelHeader(ExcelStream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> GetEPPlusExcelHeader(string filepath)
        {
            return EPPlusExcelReadTool.GetExcelHeader(filepath);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> GetEPPlusExcelHeader(Stream stream)
        {
            return EPPlusExcelReadTool.GetExcelHeader(stream);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> GetEPPlusExcelHeader(ExcelPackage pack)
        {
            return EPPlusExcelReadTool.GetExcelHeader(pack);
        }
        /// <summary>
        /// 获取表格header(仅限简单的单行表头)-EPPlus
        /// </summary>
        public IEnumerable<string> GetEPPlusExcelHeader(ExcelWorksheet sheet)
        {
            return EPPlusExcelReadTool.GetExcelHeader(sheet);
        }



        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetEPPlusExcelDataAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await EPPlusExcelReadTool.GetExcelDataAsync(ExcelStream);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetEPPlusExcelDataAsync(string filepath)
        {
            return await EPPlusExcelReadTool.GetExcelDataAsync(filepath);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetEPPlusExcelDataAsync(Stream stream)
        {
            return await EPPlusExcelReadTool.GetExcelDataAsync(stream);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetEPPlusExcelDataAsync(ExcelPackage pack)
        {
            return await EPPlusExcelReadTool.GetExcelDataAsync(pack);
        }
        /// <summary>
        /// 获取表格的数据(仅限简单的单行表头)-EPPlus
        /// </summary>
        public async Task<IEnumerable<IEnumerable<string>>> GetEPPlusExcelDataAsync(ExcelWorksheet sheet)
        {
            return await EPPlusExcelReadTool.GetExcelDataAsync(sheet);
        }



        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> GetEPPlusExcelHeaderByOptions()
        {
            if (ExcelStream == null)
                throw new Exception();
            return EPPlusExcelReadTool.GetExcelHeaderByOptions(ExcelStream, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> GetEPPlusExcelHeaderByOptions(string filepath)
        {
            return EPPlusExcelReadTool.GetExcelHeaderByOptions(filepath, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> GetEPPlusExcelHeaderByOptions(Stream stream)
        {
            return EPPlusExcelReadTool.GetExcelHeaderByOptions(stream, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> GetEPPlusExcelHeaderByOptions(ExcelPackage pack)
        {
            return EPPlusExcelReadTool.GetExcelHeaderByOptions(pack, this);
        }
        /// <summary>
        /// 获取表头及其index-EPPlus
        /// </summary>
        public Dictionary<string, int> GetEPPlusExcelHeaderByOptions(ExcelWorksheet sheet)
        {
            return EPPlusExcelReadTool.GetExcelHeaderByOptions(sheet, this);
        }



        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> GetEPPlusExcelDataByOptionsAsync()
        {
            if (ExcelStream == null)
                throw new Exception();
            return await EPPlusExcelReadTool.GetExcelDataByOptionsAsync(ExcelStream, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> GetEPPlusExcelDataByOptionsAsync(string filepath)
        {
            return await EPPlusExcelReadTool.GetExcelDataByOptionsAsync(filepath, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> GetEPPlusExcelDataByOptionsAsync(Stream stream)
        {
            return await EPPlusExcelReadTool.GetExcelDataByOptionsAsync(stream, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> GetEPPlusExcelDataByOptionsAsync(ExcelPackage pack)
        {
            return await EPPlusExcelReadTool.GetExcelDataByOptionsAsync(pack, this);
        }
        /// <summary>
        /// 根据配置 获取表格数据-EPPlus
        /// </summary>
        public async Task<string[][]> GetEPPlusExcelDataByOptionsAsync(ExcelWorksheet sheet)
        {
            return await EPPlusExcelReadTool.GetExcelDataByOptionsAsync(sheet, this);
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