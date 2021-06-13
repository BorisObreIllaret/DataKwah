using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataKwah.Domain.Entities;

namespace DataKwah.Persistence.Repositories.Product
{
    public interface IReviewRepository
    {
        Task<Tuple<List<Review>, int>> FilterReviews(ReviewQueryObject queryObject, CancellationToken cancellationToken = default);
    }
}