using eCommerce.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.BusinessLogicLayer.Validators
{
    public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator()
        {
            RuleFor(temp => temp.ProductName).NotEmpty().WithMessage("Product Name can't be blank");
            RuleFor(temp => temp.Category).IsInEnum().WithMessage("Category should be provided");
            RuleFor(temp => temp.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit price should be between 0 and {double.MaxValue}");
            RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity should between 0 and {int.MaxValue}");
        }
    }
}
