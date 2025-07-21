namespace App_Repositories;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(); //geriye etkilenen satır sayısını dönecek ondan dolayı int
}
