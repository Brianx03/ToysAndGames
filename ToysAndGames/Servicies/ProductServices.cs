using ToysAndGamesDataAccess.Data;
using ToysAndGamesModel.Models;

namespace ToysAndGames.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext _db;
        public ProductServices(ApplicationDbContext db)
        {
            this._db = db;
        }

        public List<Product> Get()
        {
            return _db.Products.ToList();
        }

        public Product Insert(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return product;
        }

        public int Delete(int id)
        {
            var p = _db.Products.Find(id);
            if (p != null)
            {
                _db.Products.Remove(p);
                _db.SaveChanges();
                return p.Id;
            }
            return 0;
        }
    }
}
