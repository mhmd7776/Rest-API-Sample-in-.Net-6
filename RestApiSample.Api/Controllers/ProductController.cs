using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiSample.Api.Data.DTOs;
using RestApiSample.Api.Data.Models;
using RestApiSample.Api.Repositories.Interfaces;

namespace RestApiSample.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ProductController : ControllerBase
    {
        #region Ctor

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        #endregion

        #region Get Requests

        /// <summary>
        /// Get list of all products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetProductsAsync();

            var result = _mapper.Map<List<ProductDto>>(products);

            return Ok(result);
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="productId">The Id of product</param>
        /// <returns></returns>
        [HttpGet("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null) return NotFound();

            var result = _mapper.Map<ProductDto>(product);

            return Ok(result);
        }

        #endregion

        #region Post Requests

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="createProductDto">The DTO for create product</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto? createProductDto)
        {
            if (createProductDto == null) return BadRequest();

            var product = _mapper.Map<Product>(createProductDto);

            await _productRepository.CreateProductAsync(product);

            var result = _mapper.Map<ProductDto>(product);

            return CreatedAtAction("GetProduct", "Product", new { productId = product.ProductId, version = HttpContext.GetRequestedApiVersion()?.ToString() }, result);
        }

        #endregion

        #region Patch Requests

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="productId">The Id of product</param>
        /// <param name="updateProductDto">The DTO for update product</param>
        /// <returns></returns>
        [HttpPatch("{productId:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDto? updateProductDto)
        {
            if (updateProductDto == null) return BadRequest();

            if (!await _productRepository.IsProductExistsByIdAsync(productId) ||
                productId != updateProductDto.ProductId)
            {
                return NotFound();
            }

            var product = _mapper.Map<Product>(updateProductDto);

            await _productRepository.UpdateProductAsync(product);

            return NoContent();
        }

        #endregion

        #region Delete Requests

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="productId">The Id of product</param>
        /// <returns></returns>
        [HttpDelete("{productId:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productRepository.DeleteProductByIdAsync(productId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion
    }
}
