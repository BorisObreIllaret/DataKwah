using FluentValidation;

namespace DataKwah.Application.Commands.Product.IndexOne
{
    public class IndexOneValidator : AbstractValidator<IndexOneRequest>
    {
        public IndexOneValidator()
        {
            RuleFor(req => req.Asin).NotEmpty();
        }
    }
}
