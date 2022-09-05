using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestApiSample.Web.Data.ViewModels;
using RestApiSample.Web.Repositories.Interfaces;
using RestApiSample.Web.Utilities.Extensions;

namespace RestApiSample.Web.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        #region Ctor

        private readonly IHttpClientFactory _client;

        public ProductRepository(IHttpClientFactory client)
        {
            _client = client;
        }

        #endregion

        public async Task<List<ProductViewModel>?> GetAllProductsAsync(string url)
        {
            // create the request
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            // create client
            var client = _client.CreateClient();

            // send request and get response
            var response = await client.SendAsync(request);

            // check response validation
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            // get the result as json
            var jsonResult = await response.Content.ReadAsStringAsync();

            // Deserialize the jsonResult and get the valid result
            var result = JsonConvert.DeserializeObject<List<ProductViewModel>>(jsonResult);

            return result;
        }

        public async Task<ProductViewModel?> GetProductByIdAsync(string url, int id)
        {
            // create the request
            var request = new HttpRequestMessage(HttpMethod.Get, url.AddIdToApiUrl(id.ToString()));

            // create client
            var client = _client.CreateClient();

            // send request and get response
            var response = await client.SendAsync(request);

            // check response validation
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            // get the result as json
            var jsonResult = await response.Content.ReadAsStringAsync();

            // Deserialize the jsonResult and get the valid result
            var result = JsonConvert.DeserializeObject<ProductViewModel>(jsonResult);

            return result;
        }

        public async Task<bool> CreateProductAsync(string url, CreateProductViewModel createProductViewModel)
        {
            // create the request
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            // add content of request
            request.Content = new StringContent(JsonConvert.SerializeObject(createProductViewModel), Encoding.UTF8, "application/json");

            // create client
            var client = _client.CreateClient();

            // send request and get response
            var response = await client.SendAsync(request);

            // check response validation
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateProductAsync(string url, int id, UpdateProductViewModel updateProductViewModel)
        {
            // create the request
            var request = new HttpRequestMessage(HttpMethod.Patch, url.AddIdToApiUrl(id.ToString()));

            // add content of request
            request.Content = new StringContent(JsonConvert.SerializeObject(updateProductViewModel), Encoding.UTF8, "application/json");

            // create client
            var client = _client.CreateClient();

            // send request and get response
            var response = await client.SendAsync(request);

            // check response validation
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProductByIdAsync(string url, int id)
        {
            // create the request
            var request = new HttpRequestMessage(HttpMethod.Delete, url.AddIdToApiUrl(id.ToString()));

            // create client
            var client = _client.CreateClient();

            // send request and get response
            var response = await client.SendAsync(request);

            // check response validation
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                return false;
            }

            return true;
        }
    }
}
