namespace App_Services.Products.Dto;
public class ProductDto
{
    //init diyerek 
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public decimal Price { get; init; }
    public int Stock { get; init; }

}
