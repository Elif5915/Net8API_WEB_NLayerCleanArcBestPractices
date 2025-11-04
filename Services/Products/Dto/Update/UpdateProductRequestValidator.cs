using FluentValidation;

namespace App_Services.Products.Dto.Update;
public  class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Ürün ismi boş geçilemez!")
           .Length(3, 15).WithMessage("Ürün ismi 3 ile 15 karakter arasında olmalıdır.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");
    }

}
