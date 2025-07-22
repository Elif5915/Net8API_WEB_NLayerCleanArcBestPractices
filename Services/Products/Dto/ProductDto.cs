namespace App_Services.Products.Dto;

// böyle class ile değilde .net 9 ile gelen recordlar ile de yapılabilr.

//public record ProductDto
//{
//    public int Id { get; init; }
//    public string Name { get; init; } = default!;
//    public decimal Price { get; init; }
//    public int Stock { get; init; }

//} yerine primary constructor şeklinde de yazabilrisn.
public record ProductDto(int Id,string Name,decimal Price,int Stock);


//public class ProductDto
//{
//    //init diyerek oluşan nesnedeki dataların sonradan değiştirilmesini engelleriz.
//    //eğer data değişmesi gerekse yeniden nesne ürettirecek ve orada yapmalı değişimi
//    //init .net 9 ile birlikte gelen keyword, class libary arasında taşınan dataları mümkün olduğunca
//    //immutability(değiştirilemez) olması gerek.
//    public int Id { get; init; }
//    public string Name { get; init; } = default!;
//    public decimal Price { get; init; }
//    public int Stock { get; init; }

//}
