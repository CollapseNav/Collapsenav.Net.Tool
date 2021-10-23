using System.Collections.Generic;
using System.Linq;

namespace Collapsenav.Net.Tool.Data
{
    public class PageData : PageData<object>
    {
    }
    public class PageData<T>
    {
        public int? Total { get; set; }
        public int? Length { get => Data?.Count(); }
        public IEnumerable<T> Data { get; set; }
        public PageData() { }
        public PageData(int? total, IEnumerable<T> data)
        {
            Total = total;
            Data = data;
        }
    }
}
