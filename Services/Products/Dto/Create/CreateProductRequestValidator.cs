using App_Repositories.Product;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace App_Services.Products.Dto.Create;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Ürün ismi boş geçilemez!")
           .Length(3, 15).WithMessage("Ürün ismi 3 ile 15 karakter arasında olmalıdır.")
        //1.YONTE
        //.Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında/sistemde bulunmaktadır."); //business ile ilgili bir validasyonu senkron olarak db gidip check ettik, bu sistem yük altında performansa zarar verecektir.  Bunu ne zaman kullanacaksınız; kurum içinde bir app yapıyorsanız çünkü kurum içinde çok fazla request gelmez. bu yüzden ilgili kodların bir arada bulunmuş olur. 
        //3.YONTEM
        .MustAsync(MustUniqueProductNameAsync).WithMessage("Ürün ismi veritabanında/sistemde bulunmaktadır.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");
    }

    #region 1.YONTEM SENKRON VALİDASYON
    //private bool MustUniqueProductName(string name)
    //{
    //    return !_productRepository.Where(x => x.Name == name).Any();

    //    // false => bir hata var
    //    // true => bir hata yok
    //}
    #endregion

    #region 3.YONTEM ASENKRON VALİDASYON AMA BU BİZE ÇOK UYMAYAN İLLA BURADA ASYNC YAZMAK İSTEDİĞİN ÖRNEĞİ
    private async Task<bool> MustUniqueProductNameAsync(string name, CancellationToken cancellationToken)
    {
        return !await _productRepository.Where(x => x.Name == name).AnyAsync(cancellationToken);
    }

    // bu 3.yontemli kod yazıyorsan, .net core default pipelinenında malesef bu kod çalışmıyor. bu kodu yazıyorsan sen default .net core pipeline çalıştırmayacaksın o zaman.
    // yani otomatik validasyon işlemini gerçekleştirmeyeceksin yani serviceextensiondaki manuel AddFluentValidationAutoValidation kapatacaksın.Ama bunu kapatınca işler biraz çirkinleşir.
    #endregion
}
