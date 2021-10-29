using System;
using System.Collections.Generic;
using System.Linq;
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
        public static ReadConfig<T> Default<T, E>(this ReadConfig<T> origin, Expression<Func<T, E>> prop, E defaultValue = default)
        {
            origin.FieldOption = origin.FieldOption.Append(GenOption(string.Empty, prop, item => defaultValue));
            return origin;
        }

        public static ReadConfig<T> Require<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            var member = prop.GetMember();
            if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();

            var option = GenOption(field, prop, action);
            action = option.Action;
            option.Action = item =>
            {
                if (item.IsEmpty())
                    throw new Exception($@" {field} 不可为空");
                return action(item);
            };

            origin.FieldOption = origin.FieldOption.Append(option);
            return origin;
        }
        public static ReadConfig<T> Add<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();
            origin.FieldOption = origin.FieldOption.Append(GenOption(field, prop, action));
            return origin;
        }
        public static ReadConfig<T> Add<T, E>(this ReadConfig<T> origin, string field, bool check, Expression<Func<T, E>> prop, Func<string, object> action = null)
        {
            if (check)
            {
                if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();
                origin.FieldOption = origin.FieldOption.Append(GenOption(field, prop, action));
            }
            return origin;
        }
        public static ReadConfig<T> AddInit<T>(this ReadConfig<T> origin, Func<T, T> action = null)
        {
            origin.Init = action;
            return origin;
        }


        public static ExportConfig<T> Add<T>(this ExportConfig<T> orgion, string field, Func<T, object> action)
        {
            orgion.FieldOption = orgion.FieldOption.Append(new ExportCellOption<T>
            {
                ExcelField = field,
                Action = action
            });
            return orgion;
        }
        public static ExportConfig<T> Add<T>(this ExportConfig<T> orgion, string field, bool check, Func<T, object> action)
        {
            if (check)
            {
                orgion.FieldOption = orgion.FieldOption.Append(new ExportCellOption<T>
                {
                    ExcelField = field,
                    Action = action
                });
            }
            return orgion;
        }
    }
}
