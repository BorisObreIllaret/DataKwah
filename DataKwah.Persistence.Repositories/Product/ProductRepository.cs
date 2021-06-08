using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task Add(Domain.Entities.Product product, CancellationToken cancellationToken)
        {
            await WritableProducts.AddAsync(product, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(IEnumerable<Domain.Entities.Product> products, CancellationToken cancellationToken)
        {
            await WritableProducts.AddRangeAsync(products, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> IsAnyProductById(int id, CancellationToken cancellationToken)
        {
            return await ReadableProducts.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> IsAnyProductByAsin(string asin, CancellationToken cancellationToken)
        {
            return await ReadableProducts.AnyAsync(x => x.Asin == asin, cancellationToken);
        }
    }
}
