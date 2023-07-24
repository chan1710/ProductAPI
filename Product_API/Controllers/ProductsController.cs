using Microsoft.AspNetCore.Mvc;
using Product_API.Services;

namespace Product_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRespository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRespository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts(string search, double? from, double? to, string sortBy, int page = 1)
        {
            try
            {
                var result = _productRespository.GetAll(search, from, to, sortBy, page = 1);
                // hkashjdf
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
