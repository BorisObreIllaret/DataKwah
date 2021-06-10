using System.Collections.Generic;

namespace DataKwah.Core.Filter
{
    public interface IFilterResponse<T> where T : class, new()
    {
        IEnumerable<T> Items { get; set; }
        int Count { get; set; }
    }
}
