using System;
using System.Linq;

namespace DataKwah.Core.Extensions.System.Linq
{
    // ReSharper disable once InconsistentNaming
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySkipAndTake<T>(this IQueryable<T> query, int? page, int? limit)
        {
            if (page == null || limit == null) return query;
            var take = Math.Max(0, limit.Value);
            var skip = Math.Max(0, page.Value) * take;
            query = query.Skip(skip).Take(take);
            return query;
        }
    }
}