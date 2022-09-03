using Microsoft.EntityFrameworkCore;
using RestApiSample.Api.Data.DbContext;
using RestApiSample.Api.Data.Models;
using RestApiSample.Api.Repositories.Interfaces;

namespace RestApiSample.Api.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            await Save();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);

            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            var product = await GetProductByIdAsync(id);

            if (product == null) return false;

            await DeleteProductAsync(product);

            return true;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(s => s.ProductId == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> IsProductExistsByIdAsync(int id)
        {
            return await _context.Products.AnyAsync(s => s.ProductId == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);

            await Save();
        }
    }
}
