using FluentValidation;

namespace DataKwah.Application.Commands.Product.IndexMany
{
    public class IndexManyValidator : AbstractValidator<IndexManyRequest>
    {
        public IndexManyValidator()
        {
            RuleFor(req => req.Asins).NotEmpty();
        }
    }
}
