using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataKwah.Core.Extensions.System.Linq;
using DataKwah.Domain.Entities;
using DataKwah.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataKwah.Persistence.Repositories.Product
{
    public class ReviewRepository : IReviewRepository
    {
        public ReviewRepository(DataKwahDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private DataKwahDbContext DbContext { get; }

        private DbSet<Review> WritableReviews => DbContext.Reviews;
        private IQueryable<Review> ReadableReviews => WritableReviews.AsNoTracking();

        public async Task<Tuple<List<Review>, int>> FilterReviews(ReviewQueryObject queryObject, CancellationToken cancellationToken = default)
        {
            var count = await ApplyFilter(queryObject, true).CountAsync(cancellationToken);
            var items = await ApplyFilter(queryObject).ToListAsync(cancellationToken);
            return new Tuple<List<Review>, int>(items, count);
        }

        private IQueryable<Review> ApplyFilter(ReviewQueryObject queryObject, bool ignoreSkipAndTake = false)
        {
            if (queryObject == null) return ReadableReviews;

            var query = queryObject.UseWritable
                ? WritableReviews
                : ReadableReviews;

            // Includes
            if (queryObject.IncludeProduct) query = query.Include(review => review.Product);

            // Filters
            if (queryObject.ProductId.HasValue) query = query.Where(review => review.ProductId == queryObject.ProductId.Value);

            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                var search = queryObject.Search.Trim().ToUpper();
                query = query.Where(review => review.Title.ToUpper().Contains(search) || review.Body.ToUpper().Contains(search));
            }

            if (!ignoreSkipAndTake)
            {
                // Manage order
                if (!string.IsNullOrWhiteSpace(queryObject.Sort))
                    query = queryObject.Sort.ToLowerInvariant() switch
                    {
                        "asin" => queryObject.AscendingOrder ? query.OrderBy(product => product.Asin) : query.OrderByDescending(product => product.Asin),
                        "date" => queryObject.AscendingOrder ? query.OrderBy(product => product.Date) : query.OrderByDescending(product => product.Date),
                        "rating" => queryObject.AscendingOrder ? query.OrderBy(product => product.Rating) : query.OrderByDescending(product => product.Rating),
                        "title" => queryObject.AscendingOrder ? query.OrderBy(product => product.Title) : query.OrderByDescending(product => product.Title),
                        _ => query
                    };

                // Must be the last function
                query = query.ApplySkipAndTake(queryObject.Page, queryObject.Limit);
            }

            return query;
        }
    }
}
