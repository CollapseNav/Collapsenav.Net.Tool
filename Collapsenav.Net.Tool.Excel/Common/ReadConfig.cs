using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 表格读取设置
    /// </summary>
    public partial class ReadConfig<T>
    {
        private readonly Stream ExcelStream;
        /// <summary>
        /// 一行数据的读取设置
        /// </summary>
        public virtual IEnumerable<ReadCellOption> ReadOption { get; protected set; }
        /// <summary>
        /// 读取成功之后调用的针对T的委托
        /// </summary>
        public Func<T, T> Init { get; set; }
        public ReadConfig()
        {
            ReadOption = new List<ReadCellOption>();
            Init = null;
            ExcelStream = new MemoryStream();
        }
        /// <summary>
        /// 根据文件路径的初始化
        /// </summary>
        /// <param name="filepath"> 文件路径 </param>
        /// <returns></returns>
        public ReadConfig(string filepath) : this()
        {
            filepath.IsXls();
            using var fs = new FileStream(filepath, FileMode.Open);
            fs.CopyTo(ExcelStream);
        }
        /// <summary>
        /// 根据文件流的初始化
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public ReadConfig(Stream stream) : this()
        {
            stream.CopyTo(ExcelStream);
        }

        /// <summary>
        /// 添加默认单元格读取设置(其实就是不读取Excel直接给T的某个字段赋值)
        /// </summary>
        /// <param name="prop">T的属性</param>
        /// <param name="defaultValue">默认值</param>
        public virtual ReadConfig<T> Default<E>(Expression<Func<T, E>> prop, E defaultValue)
        {
            ReadOption = ReadOption.Append(GenOption(string.Empty, prop, item => defaultValue));
            return this;
        }
        /// <summary>
        /// check条件为True时添加默认单元格读取设置(其实就是不读取Excel直接给T的某个字段赋值)
        /// </summary>
        /// <param name="check">判断条件</param>
        /// <param name="prop">T的属性</param>
        /// <param name="defaultValue">默认值</param>
        public virtual ReadConfig<T> DefaultIf<E>(bool check, Expression<Func<T, E>> prop, E defaultValue)
        {
            return check ? Default(prop, defaultValue) : this;
        }
        /// <summary>
        /// 添加必填的不能为空的读取设置
        /// </summary>
        /// <param name="field">表头列</param>
        /// <param name="prop">T的属性</param>
        /// <param name="action">对单元格字符串的操作</param>
        public virtual ReadConfig<T> Require<E>(string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            var option = GenOption(field, prop, action);
            action = option.Action;
            option.Action = item =>
            {
                if (item.IsEmpty())
                    throw new NoNullAllowedException($@" {field} 不可为空");
                return action(item);
            };
            ReadOption = ReadOption.Append(option);
            return this;
        }
        /// <summary>
        /// check条件为True时添加必填的不能为空的读取设置
        /// </summary>
        /// <param name="check">判断条件</param>
        /// <param name="field">表头列</param>
        /// <param name="prop">T的属性</param>
        /// <param name="action">对单元格字符串的操作</param>
        public virtual ReadConfig<T> RequireIf<E>(bool check, string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            return check ? Require(field, prop, action) : this;
        }
        /// <summary>
        /// 添加普通单元格设置
        /// </summary>
        /// <param name="field">表头列</param>
        /// <param name="prop">T的属性</param>
        /// <param name="action">对单元格字符串的操作</param>
        public virtual ReadConfig<T> Add<E>(string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            ReadOption = ReadOption.Append(GenOption(field, prop, action));
            return this;
        }
        /// <summary>
        /// check条件为True时添加单元格设置
        /// </summary>
        /// <param name="check">判断条件</param>
        /// <param name="field">表头列</param>
        /// <param name="prop">T的属性</param>
        /// <param name="action">对单元格字符串的操作</param>
        public virtual ReadConfig<T> AddIf<E>(bool check, string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            return check ? Add(field, prop, action) : this;
        }
        public virtual ReadConfig<T> AddInit(Func<T, T> action)
        {
            Init = action;
            return this;
        }
        public virtual ReadConfig<T> AddInitIf(bool check, Func<T, T> action)
        {
            return check ? AddInit(action) : this;
        }
        /// <summary>
        /// 生成单元格设置
        /// </summary>
        /// <param name="field"></param>
        /// <param name="prop"></param>
        /// <param name="action"></param>
        public virtual ReadCellOption GenOption<E>(string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            var member = prop.GetMember();
            // 暂时还想不到其他简单的高效的方法
            // TODO 考虑提到 Tool 的 TypeTool 中 ?
            action ??= typeof(E).Name switch
            {
                nameof(String) => (item) => item,
                nameof(Int16) => (item) => short.Parse(item),
                nameof(Int32) => (item) => int.Parse(item),
                nameof(Int64) => (item) => long.Parse(item),
                nameof(Double) => (item) => double.Parse(item),
                nameof(Single) => (item) => float.Parse(item),
                nameof(Decimal) => (item) => decimal.Parse(item),
                nameof(Boolean) => (item) => bool.Parse(item),
                _ => (item) => item,
            };
            return new ReadCellOption
            {
                PropName = member.Name,
                Prop = (PropertyInfo)member,
                ExcelField = field,
                Action = action
            };
        }
    }
}
