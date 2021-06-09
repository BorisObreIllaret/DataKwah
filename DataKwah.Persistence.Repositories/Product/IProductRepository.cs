using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataKwah.Persistence.Repositories.Product
{
    public interface IProductRepository
    {
        Task Add(Domain.Entities.Product product, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<Domain.Entities.Product> products, CancellationToken cancellationToken = default);
        Task<Domain.Entities.Product> GetProductByAsin(string asin, CancellationToken cancellationToken = default);
        Task<bool> IsAnyProductById(int id, CancellationToken cancellationToken = default);
        Task<bool> IsAnyProductByAsin(string asin, CancellationToken cancellationToken = default);
        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}
