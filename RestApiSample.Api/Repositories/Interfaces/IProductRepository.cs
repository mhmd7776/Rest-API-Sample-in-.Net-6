using RestApiSample.Api.Data.Models;

namespace RestApiSample.Api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<bool> IsProductExistsByIdAsync(int id);

        Task UpdateProductAsync(Product product);

        Task CreateProductAsync(Product product);

        Task<bool> DeleteProductByIdAsync(int id);

        Task DeleteProductAsync(Product product);

        Task Save();
    }
}
