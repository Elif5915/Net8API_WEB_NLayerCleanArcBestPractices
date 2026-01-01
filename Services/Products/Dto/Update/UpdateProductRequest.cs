namespace App_Services.Products.Dto.Update;
public record UpdateProductRequest(string Name, decimal Price, int Stock, int categoryId);
