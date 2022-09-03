using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApiSample.Api.Data.DTOs;
using RestApiSample.Api.Repositories.Interfaces;

namespace RestApiSample.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("2.0")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ProductV2Controller : ControllerBase
    {
        #region Ctor

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductV2Controller(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        #endregion

        #region Get Requests

        /// <summary>
        /// Get list of all products in v2 controller.
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

        #endregion
    }
}
