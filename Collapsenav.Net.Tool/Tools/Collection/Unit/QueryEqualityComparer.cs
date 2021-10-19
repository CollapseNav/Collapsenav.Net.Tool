using System;
using System.Collections.Generic;

namespace Collapsenav.Net.Tool
{
    public class QueryEqualityComparer<T> : IEqualityComparer<T>
    {
        public QueryEqualityComparer(Func<T, int> hashcodeFunc)
        {
            HashCodeFunc = hashcodeFunc;
        }
        public QueryEqualityComparer(Func<T, T, bool> equalFunc, Func<T, int> hashcodeFunc)
        {
            EqualFunc = equalFunc;
            HashCodeFunc = hashcodeFunc;
        }
        public QueryEqualityComparer(Func<T, T, bool> equalFunc)
        {
            EqualFunc = equalFunc;
        }
        private readonly Func<T, T, bool> EqualFunc;
        private readonly Func<T, int> HashCodeFunc;

        public bool Equals(T x, T y)
        {
            return EqualFunc == null ? GetHashCode(x) == GetHashCode(y) : EqualFunc(x, y);
        }

        public int GetHashCode(T obj)
        {
            return HashCodeFunc == null ? obj.GetHashCode() : HashCodeFunc(obj);
        }
    }
}
