using DataKwah.Persistence.Repositories.Product;
using FluentValidation;

namespace DataKwah.Application.Queries.Product.Details
{
    public class DetailsValidator : AbstractValidator<DetailsRequest>
    {
        public DetailsValidator(IProductRepository productRepository)
        {
            RuleFor(req => req.ProductId)
                .MustAsync(async (productId, cancellationToken) => await productRepository.IsAnyProductById(productId, cancellationToken))
                .WithMessage(productId => $"Unknown product with ID '${productId}'");
        }
    }
}