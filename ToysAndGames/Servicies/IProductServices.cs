using Microsoft.AspNetCore.Mvc;
using ToysAndGames_Model.Models;

namespace ToysAndGames.Services
{
    public interface IProductServices
    {
        List<Product> Get();
        Product Insert(Product product);
        Product Update(Product product);
        void Delete(int id);
    }
}
