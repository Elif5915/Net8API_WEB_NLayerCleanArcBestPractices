namespace App_Repositories.Categories;
using App_Repositories.Product;
using System;

public  class Category : IAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}
