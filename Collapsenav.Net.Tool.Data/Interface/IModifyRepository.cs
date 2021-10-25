using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool.Data
{
    public interface IModifyRepository<T> : IQueryRepository<T>
    {
        Task<int> AddAsync(IEnumerable<T> entityList);
        Task<int> DeleteAsync(Expression<Func<T, bool>> exp, bool isTrue = false);
        Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
    }
}
