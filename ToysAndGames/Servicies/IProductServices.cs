using ToysAndGamesModel.Models;

namespace ToysAndGames.Services
{
    public interface IProductServices
    {
        Task<List<Product>> Get();
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task<int> Delete(int id);
    }
}
