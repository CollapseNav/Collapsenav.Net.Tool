using System;
using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool
{
    public partial class TypeTool
    {
        /// <summary>
        /// 是否内建(基本)类型
        /// </summary>
        public static bool IsBuildInType(Type type)
        {
            return type.Name switch
            {
                nameof(Boolean) => true,
                nameof(Byte) => true,
                nameof(SByte) => true,
                nameof(Char) => true,
                nameof(Decimal) => true,
                nameof(Double) => true,
                nameof(Single) => true,
                nameof(Int32) => true,
                nameof(UInt32) => true,
                nameof(IntPtr) => true,
                nameof(UIntPtr) => true,
                nameof(Int64) => true,
                nameof(UInt64) => true,
                nameof(Int16) => true,
                nameof(UInt16) => true,
                nameof(String) => true,
                nameof(DateTime) => true,
                _ => false,
            };
        }
        /// <summary>
        /// 是否内建(基本)类型
        /// </summary>
        public static bool IsBuildInType<T>()
        {
            return IsBuildInType(typeof(T));
        }
        /// <summary>
        /// 是否内建(基本)类型
        /// </summary>
        public static bool IsBaseType<T>()
        {
            return IsBuildInType(typeof(T));
        }
        /// <summary>
        /// 是否内建(基本)类型
        /// </summary>
        public static bool IsBaseType(Type type)
        {
            return IsBuildInType(type);
        }

        /// <summary>
        /// 获取属性名称
        /// </summary>
        public static IEnumerable<string> PropNames(Type type)
        {
            return type.GetProperties().Select(item => item.Name);
        }
        /// <summary>
        /// 获取属性名称
        /// </summary>
        public static IEnumerable<string> PropNames<T>()
        {
            return PropNames(typeof(T));
        }
        /// <summary>
        /// 获取属性名称
        /// </summary>
        public static IEnumerable<string> PropNames(Type type, int depth)
        {
            var props = type.GetProperties();
            var nameProps = props.Where(item => item.PropertyType.IsBuildInType());
            var loopProps = props.Where(item => !item.PropertyType.IsBuildInType());
            if (depth > 0)
                return loopProps.Select(item => PropNames(item.PropertyType, depth - 1).Select(propName => $@"{item.Name}.{propName}")).Merge(nameProps.Select(item => item.Name));
            return nameProps.Select(item => item.Name).Concat(loopProps.Select(item => item.Name));
        }
        /// <summary>
        /// 获取属性名称
        /// </summary>
        public static IEnumerable<string> PropNames<T>(int? depth)
        {
            return PropNames(typeof(T), depth ?? 0);
        }

        /// <summary>
        /// 获取内建类型的属性名
        /// </summary>
        public static IEnumerable<string> BuildInTypePropNames<T>()
        {
            return BuildInTypePropNames(typeof(T));
        }

        /// <summary>
        /// 获取内建类型的属性名
        /// </summary>
        public static IEnumerable<string> BuildInTypePropNames(Type type)
        {
            var props = type.GetProperties();
            var nameProps = props.Where(item => item.PropertyType.IsBuildInType());
            return nameProps.Select(item => item.Name);
        }
        /// <summary>
        /// 就……GetValue
        /// </summary>
        public static object GetValue<T>(object obj, string field)
        {
            var prop = typeof(T).GetProperties().Where(item => item.Name == field && item.PropertyType.IsBuildInType()).FirstOrDefault();
            if (prop == null)
                return "";
            return prop.GetValue(obj);
        }
    }
}
