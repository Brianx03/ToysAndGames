using ToysAndGames_DataAccess.Data;
using ToysAndGames_Model.Models;

namespace ToysAndGames.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext db;
        public ProductServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Product> Get()
        {
            return db.Products.ToList();
        }

        public Product Insert(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            db.Products.Update(product);
            db.SaveChanges();
            return product;
        }

        public void Delete(int id)
        {
            var p = db.Products.FirstOrDefault(p => p.Id == id);
            db.Products.Remove(p);
            db.SaveChanges();
        }
    }
}
