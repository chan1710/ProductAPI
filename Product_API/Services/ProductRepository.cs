using Microsoft.EntityFrameworkCore;
using Product_API.Data;
using Product_API.Models;

namespace Product_API.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProducts = _context.Products.Include(p => p.Type).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(p => p.ProductName.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(p => p.Price >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(p => p.Price <= to);
            }
            #endregion

            #region Sorting
            // Default sort by Name (TenHh)
            allProducts = allProducts.OrderBy(p => p.ProductName);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "ProductName_desc":
                        allProducts = allProducts.OrderByDescending(p => p.ProductName);
                        break;
                    case "Price_asc":
                        allProducts = allProducts.OrderBy(p => p.Price);
                        break;
                    case "Price_desc":
                        allProducts = allProducts.OrderByDescending(p => p.Price);
                        break;
                }
            }
            #endregion

            /*
            #region Paging
            allProducts = allProducts.Skip((page -1)*PAGE_SIZE).Take(PAGE_SIZE);
            #endregion

            var result = allProducts.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                TypeName = p.Type.TypeName
            });
            return result.ToList();
            */


            var result = PaginatedList<Data.Product>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                TypeName = p.Type.TypeName
            }).ToList();
        }
    }
}
