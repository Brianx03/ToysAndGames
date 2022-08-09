using ToysAndGamesDataAccess.Data;
using ToysAndGamesModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ToysAndGames.Services
{
    public class ProductServices : IProductServices
    {
        //TODO: usually the variable is named context instead of _db
        //TODO: Use try catch for the services operations
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
            //TODO: use variables with meanings
            var p = await _db.Products.FindAsync(id);
            //TODO: try to use the new naming for is null or is not null
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
