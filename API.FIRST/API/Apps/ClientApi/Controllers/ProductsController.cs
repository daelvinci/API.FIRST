using API.Apps.AdminApi.DTOs.ProductDTOs;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Apps.ClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ProductsController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetAll(ProductGetAllDTO prAll )
        {
            var datas = _context.Products.ToList();

            List<ProductGetAllDTO> products = datas.Select(x => new ProductGetAllDTO { BrandId = x.BrandId, Name = x.Name, SalePrice = x.SalePrice }).ToList();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _context.Products.Find(id);

            if (data == null) return NotFound();

            ProductGetClientDTO getDTO = new()
            {
                BrandId = data.BrandId,
                Name = data.Name,
                SalePrice = data.SalePrice
            };

            return Ok(getDTO);
        }
    }
}
