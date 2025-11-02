using FluentValidation;

namespace App_Services.Products.Dto.Update;
public  class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Ürün ismi boş geçilemez!")
           .Length(3, 15).WithMessage("Ürün ismi 3 ile 15 karakter arasında olmalıdır.")
        //1.YONTE
        //.Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında/sistemde bulunmaktadır."); //business ile ilgili bir validasyonu senkron olarak db gidip check ettik, bu sistem yük altında performansa zarar verecektir.  Bunu ne zaman kullanacaksınız; kurum içinde bir app yapıyorsanız çünkü kurum içinde çok fazla request gelmez. bu yüzden ilgili kodların bir arada bulunmuş olur. 
        //3.YONTEM
        .MustAsync(MustUniqueProductNameAsync).WithMessage("Ürün ismi veritabanında/sistemde bulunmaktadır.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı sfırdan büyük olmalıdır");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");
    }

}
