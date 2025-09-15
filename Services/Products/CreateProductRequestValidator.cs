using App_Services.Products.Dto;
using FluentValidation;

namespace App_Services.Products;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotNull().WithMessage("Ürün ismi gereklidir!")
           .NotEmpty().WithMessage("Ürün ismi boş geçilemez!")
           .Length(3, 15).WithMessage("Ürün ismi 3 ile 15 karakter arasında olmalıdır.");
    }


}
