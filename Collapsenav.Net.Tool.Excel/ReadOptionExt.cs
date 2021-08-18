using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;

namespace Collapsenav.Net.Tool.Excel
{
    /// <summary>
    /// 一些大概会方便的扩展方法
    /// </summary>
    public static class ExcelOptionExt
    {
        public static ReadConfig<T> Default<T, E>(this ReadConfig<T> origin, Expression<Func<T, E>> prop, E defaultValue = default)
        {
            origin.DefaultOption.Add(GenOption(string.Empty, prop, item => defaultValue));
            return origin;
        }

        public static ReadCellOption<T> GenOption<T, E>(string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            var member = prop.GetMember();
            // 暂时还想不到其他简单的高效的方法
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
            return new ReadCellOption<T>
            {
                PropName = member.Name,
                Prop = (PropertyInfo)member,
                ExcelField = field,
                Action = action
            };
        }

        public static ReadConfig<T> Require<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            var member = prop.GetMember();
            if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();

            origin.FieldOption.Add(GenOption(field, prop, item =>
            {
                if (string.IsNullOrEmpty(item))
                    throw new Exception($@" {field} 不可为空");
                return action == null ? item : action(item);
            }));
            return origin;
        }
        public static ReadConfig<T> Add<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();
            origin.FieldOption.Add(GenOption(field, prop, action));
            return origin;
        }
        public static ReadConfig<T> AddInit<T>(this ReadConfig<T> origin, Func<T, T> action = null)
        {
            origin.Init = action;
            return origin;
        }
    }
}
