using Microsoft.AspNetCore.Mvc;
using Product_API.Models;

namespace Product_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = productVM.ProductName,
                Price = productVM.Price
            };
            products.Add(product);
            return Ok(new
            {
                Success = true,
                Data = products
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                // LINQ [Object] Query
                var product = products.SingleOrDefault(p => p.ProductId == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, Product ProductEdit)
        {
            try
            {
                // LINQ [Object] Query
                var product = products.SingleOrDefault(p => p.ProductId == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }

                if (id != product.ProductId.ToString())
                {
                    return BadRequest();
                }

                // Update
                product.ProductName = ProductEdit.ProductName;
                product.Price = ProductEdit.Price;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id, Product productEdit)
        {
            try
            {
                // LINQ [Object] Query
                var product = products.SingleOrDefault(p => p.ProductId == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }

                // Delete
                products.Remove(product);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
