using Microsoft.AspNetCore.Mvc;
using ToysAndGamesModel.Models;

namespace ToysAndGames.Services
{
    public interface IProductServices
    {
        List<Product> Get();
        Product Insert(Product product);
        Product Update(Product product);
        int Delete(int id);
    }
}
