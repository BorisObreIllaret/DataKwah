using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataKwah.Core.Extensions.System.Linq;
using DataKwah.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataKwah.Persistence.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(DataKwahDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private DataKwahDbContext DbContext { get; }
        private DbSet<Domain.Entities.Product> WritableProducts => DbContext.Products;
        private IQueryable<Domain.Entities.Product> ReadableProducts => WritableProducts.AsNoTracking();

        public Task SaveChanges(CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Add(Domain.Entities.Product product, CancellationToken cancellationToken = default)
        {
            await WritableProducts.AddAsync(product, cancellationToken);
        }

        public async Task AddRange(IEnumerable<Domain.Entities.Product> products, CancellationToken cancellationToken = default)
        {
            await WritableProducts.AddRangeAsync(products, cancellationToken);
        }

        public async Task<Domain.Entities.Product> GetProductByAsin(string asin, CancellationToken cancellationToken = default)
        {
            return await WritableProducts.FirstOrDefaultAsync(product => product.Asin == asin, cancellationToken);
        }

        public async Task<Tuple<List<Domain.Entities.Product>, int>> FilterProducts(ProductQueryObject queryObject, CancellationToken cancellationToken = default)
        {
            var count = await ApplyFilter(queryObject, true).CountAsync();
            var items = await ApplyFilter(queryObject).ToListAsync();
            return new Tuple<List<Domain.Entities.Product>, int>(items, count);
        }

        public async Task<bool> IsAnyProductById(int id, CancellationToken cancellationToken = default)
        {
            return await ReadableProducts.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> IsAnyProductByAsin(string asin, CancellationToken cancellationToken = default)
        {
            return await ReadableProducts.AnyAsync(x => x.Asin == asin, cancellationToken);
        }

        private IQueryable<Domain.Entities.Product> ApplyFilter(ProductQueryObject queryObject, bool ignoreSkipAndTake = false)
        {
            if (queryObject == null) return ReadableProducts;

            var query = queryObject.UseWritable
                ? WritableProducts
                : ReadableProducts;

            // Includes
            if (queryObject.IncludeReviews) query = query.Include(product => product.Reviews);
            if (queryObject.IncludeState) query = query.Include(product => product.ProductState);

            // Filters
            if (!string.IsNullOrWhiteSpace(queryObject.Search)) query = query.Where(product => product.Label.Contains(queryObject.Search.Trim()));

            if (!ignoreSkipAndTake)
            {
                // Manage order
                if (!string.IsNullOrWhiteSpace(queryObject.Sort))
                    query = queryObject.Sort.ToLowerInvariant() switch
                    {
                        "asin" => queryObject.AscendingOrder ? query.OrderBy(product => product.Asin) : query.OrderByDescending(product => product.Asin),
                        "label" => queryObject.AscendingOrder ? query.OrderBy(product => product.Label) : query.OrderByDescending(product => product.Label),
                        "state" => queryObject.AscendingOrder ? query.OrderBy(product => product.ProductState.Id) : query.OrderByDescending(product => product.ProductState.Id),
                        _ => query
                    };

                // Must be the last function
                query = query.ApplySkipAndTake(queryObject.Page, queryObject.Limit);
            }

            return query;
        }
    }
}
