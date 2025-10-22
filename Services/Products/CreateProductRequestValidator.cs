using App_Services.Products.Dto;
using FluentValidation;

namespace App_Services.Products;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Ürün ismi boş geçilemez!")
           .Length(3, 15).WithMessage("Ürün ismi 3 ile 15 karakter arasında olmalıdır.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı sfırdan büyük olmalıdır");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");
    }


}
