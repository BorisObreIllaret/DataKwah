using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataKwah.Persistence.Repositories.Product
{
    public interface IProductRepository
    {
        Task Add(Domain.Entities.Product product, CancellationToken cancellationToken);
        Task AddRange(IEnumerable<Domain.Entities.Product> products, CancellationToken cancellationToken);
        Task<bool> IsAnyProductById(int id, CancellationToken cancellationToken);
        Task<bool> IsAnyProductByAsin(string asin, CancellationToken cancellationToken);
    }
}
