using Microsoft.AspNetCore.Mvc;
using RestApiSample.Web.Data.ViewModels;
using RestApiSample.Web.Repositories.Interfaces;
using RestApiSample.Web.Utilities.Extensions;
using RestApiSample.Web.Utilities.Statics;

namespace RestApiSample.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor

        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Products List

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync(StaticDetails.ProductsApiUrl);

            if (products == null)
            {
                return View("ApiNotResponse");
            }

            return View(products);
        }

        #endregion

        #region Create Product

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel createProductViewModel, IFormFile? productImage)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(createProductViewModel);
            }

            if (productImage == null)
            {
                TempData["Error"] = "The product image cannot be empty.";
                return View(createProductViewModel);
            }

            #endregion

            #region Upload Image

            var imageName = Guid.NewGuid() + Path.GetExtension(productImage.FileName);

            var validImages = new List<string> { ".jpg", ".png", ".jpeg" };

            var uploadImageResult =
                productImage.UploadFile(imageName, StaticDetails.ProductImageUploadPath, validImages);

            if (!uploadImageResult)
            {
                TempData["Error"] = "The product image format is invalid.";
                return View(createProductViewModel);
            }

            createProductViewModel.ImagePath = imageName.GenerateProductImageFullPath();

            #endregion

            var result =
                await _productRepository.CreateProductAsync(StaticDetails.ProductsApiUrl, createProductViewModel);

            if (!result)
            {
                imageName.DeleteFile(StaticDetails.ProductImageUploadPath);

                return View("ApiNotResponse");
            }

            TempData["Success"] = $"{createProductViewModel.Title} added successfully.";

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Update Product

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            #region Get Product And Check Validation

            var product = await _productRepository.GetProductByIdAsync(StaticDetails.ProductsApiUrl, id);

            if (product == null)
            {
                return NotFound();
            }

            #endregion

            var updateProductViewModel = new UpdateProductViewModel
            {
                Title = product.Title,
                Description = product.Description,
                ImagePath = product.ImagePath,
                Price = product.Price,
                ProductId = product.ProductId,
                Type = product.Type
            };

            return View(updateProductViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel updateProductViewModel, IFormFile? productImage)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(updateProductViewModel);
            }

            #endregion

            #region Upload Image

            var imageName = string.Empty;

            if (productImage != null)
            {
                imageName = Guid.NewGuid() + Path.GetExtension(productImage.FileName);

                var validImages = new List<string> { ".jpg", ".png", ".jpeg" };

                var uploadImageResult =
                    productImage.UploadFile(imageName, StaticDetails.ProductImageUploadPath, validImages);

                if (!uploadImageResult)
                {
                    TempData["Error"] = "The product image format is invalid.";
                    return View(updateProductViewModel);
                }

                updateProductViewModel.ImagePath?.Split("/").Last().DeleteFile(StaticDetails.ProductImageUploadPath);

                updateProductViewModel.ImagePath = imageName.GenerateProductImageFullPath();
            }

            #endregion

            var result = await _productRepository.UpdateProductAsync(StaticDetails.ProductsApiUrl,
                updateProductViewModel.ProductId, updateProductViewModel);

            if (!result)
            {
                if (!string.IsNullOrEmpty(imageName))
                {
                    imageName.DeleteFile(StaticDetails.ProductImageUploadPath);
                }

                return View("ApiNotResponse");
            }

            TempData["Success"] = $"{updateProductViewModel.Title} updated successfully.";

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Delete Product

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProductByIdAsync(StaticDetails.ProductsApiUrl, id);

            if (!result)
            {
                return new JsonResult(new { status = "Error", message = "An error has occurred." });
            }

            return new JsonResult(new { status = "Success", message = "The product deleted successfully." });
        }

        #endregion
    }
}