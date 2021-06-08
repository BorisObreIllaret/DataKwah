using System.Threading;
using System.Threading.Tasks;

namespace DataKwah.Application.Services.Product
{
    public interface IIndexationService
    {
        Task IndexAsin(string asin, CancellationToken cancellationToken);
    }
}