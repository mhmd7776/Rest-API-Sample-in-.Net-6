using RestApiSample.Web.Data.ViewModels;

namespace RestApiSample.Web.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductViewModel>?> GetAllProductsAsync(string url);

        Task<ProductViewModel?> GetProductByIdAsync(string url, int id);

        Task<bool> CreateProductAsync(string url, CreateProductViewModel createProductViewModel);

        Task<bool> UpdateProductAsync(string url, int id, UpdateProductViewModel updateProductViewModel);

        Task<bool> DeleteProductByIdAsync(string url, int id);
    }
}
