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
        public static ReadConfig<T> Default<T, E>(this ReadConfig<T> origin, Expression<Func<T, E>> prop, E defaultValue)
        {
            origin.DefaultOption.Add(GenOption(string.Empty, prop, item => defaultValue));
            return origin;
        }


        // public static ReadCellOption<T> GenOption<T, E>(string field, Expression<Func<T, E>> prop, ExcelToEntityType type = ExcelToEntityType.Default)
        // {
        //     Func<string, object> action = item => item;
        //     switch (type)
        //     {
        //         case ExcelToEntityType.Int32:
        //             action = item =>
        //             {
        //                 if (int.TryParse(item, out int value))
        //                     return value;
        //                 throw new Exception($@" {field} 需要是整数类型");
        //             };
        //             break;
        //         case ExcelToEntityType.Int64:
        //             action = item =>
        //             {
        //                 if (long.TryParse(item, out long value))
        //                     return value;
        //                 throw new Exception($@" {field} 需要是长整数类型");
        //             };
        //             break;
        //         case ExcelToEntityType.DateTime:
        //             action = item =>
        //             {
        //                 if (DateTime.TryParse(item, out DateTime value))
        //                     return value;

        //                 throw new Exception($@" {field} 需要是日期类型");
        //             };
        //             break;
        //         case ExcelToEntityType.Double:
        //             action = item =>
        //             {
        //                 if (double.TryParse(item, out double value))
        //                     return value;

        //                 throw new Exception($@" {field} 需要是浮点数类型");
        //             };
        //             break;
        //         case ExcelToEntityType.Default:
        //             break;
        //         default: break;
        //     }
        //     return GenOption(field, prop, action);
        // }


        public static ReadCellOption<T> GenOption<T, E>(string field, Expression<Func<T, E>> prop, Func<string, object> action)
        {
            var member = prop.GetMember();
            return new ReadCellOption<T>
            {
                PropName = member.Name,
                Prop = (PropertyInfo)member,
                ExcelField = field,
                Action = action
            };
        }

        public static ReadCellOption<T> GenOption<T, E>(string field, Expression<Func<T, E>> prop, object defaultValue = null) => GenOption(field, prop, item => defaultValue);

        public static ReadConfig<T> Require<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop, Func<string, object> action)
        {
            var member = prop.GetMember();
            if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();
            Func<string, object> optionAction = item =>
            {
                if (string.IsNullOrEmpty(item))
                    throw new Exception($@" {field} 不可为空");
                if (action == null) return item;
                return action(item);
            };
            origin.FieldOption.Add(GenOption(field, prop, optionAction));
            return origin;
        }
        // public static ReadConfig<T> Require<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop, Func<string, object> action = null)
        // {
        //     var option = GenOption(field, prop);
        //     var optionAction = option.Action;
        //     option.Action = item =>
        //     {
        //         if (item.IsNull())
        //             throw new Exception($@" {field} 不可为空");
        //         if (optionAction == null) return item;
        //         return optionAction(item);
        //     };
        //     origin.FieldOption.Add(option);
        //     return origin;
        // }
        public static ReadConfig<T> Add<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop, Func<string, object> action)
        {
            if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();
            origin.FieldOption.Add(GenOption(field, prop, action));
            return origin;
        }
        public static ReadConfig<T> Add<T, E>(this ReadConfig<T> origin, string field, Expression<Func<T, E>> prop)
        {
            if (origin.FieldOption == null) origin.FieldOption = new List<ReadCellOption<T>>();
            origin.FieldOption.Add(GenOption(field, prop));
            return origin;
        }
        public static ReadConfig<T> AddInit<T>(this ReadConfig<T> origin, Func<T, T> action = null)
        {
            origin.Init = action;
            return origin;
        }
    }
}
