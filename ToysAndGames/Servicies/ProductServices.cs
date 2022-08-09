using ToysAndGamesDataAccess.Data;
using ToysAndGamesModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ToysAndGames.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext _db;
        public ProductServices(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Product>> Get()
        {
            return await _db.Products.ToListAsync();
        }
        public async Task<Product> Insert(Product product)
        {
            product.ImageBytes = await Image(product.ImagePath);
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            product.ImageBytes = await Image(product.ImagePath);
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<int> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p != null)
            {
                _db.Products.Remove(p);
                await _db.SaveChangesAsync();
                return p.Id;
            }
            return 0;
        }
        public async Task<byte[]> Image(string path)
        {
            byte[] imageArray = await File.ReadAllBytesAsync(path);
            return imageArray;
        }
    }
}
