namespace App_Services.Products.Dto.Create;
public record CreateProductResponse(int Id);
//record aslınca compile çalıştığında class gibi algılanıp class gibi davranış sergiler, sadece record ile wraplıyarak 
//projenin başka yuerinde değişebilmesinin önüne geçiyoruz, record olan propertyi başka yerde değiştirmeye
//çalışınca compiler hata fırlatır.