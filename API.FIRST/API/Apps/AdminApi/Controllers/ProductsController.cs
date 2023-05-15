using API.Apps.AdminApi.DTOs.ProductDTOs;
using Core.Entites;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ProductsController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetAll()
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

            ProductGetDTO getDTO = new() { BrandId=data.BrandId, Name=data.Name, SalePrice=data.SalePrice, CostPrice=data.CostPrice, Id=data.Id};

            return Ok(getDTO);
        }

        [HttpPost("")]
        public IActionResult Create(ProductDTO productDTO)
        {
            Product product = new()
            {
                Name = productDTO.Name,
                BrandId = productDTO.BrandId,
                CostPrice = productDTO.CostPrice,
                SalePrice = productDTO.SalePrice
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDTO productDTO)
        {
            var existPr = _context.Products.Find(id);

            if (existPr == null) return NotFound();

            existPr.BrandId = productDTO.BrandId;
            existPr.Name = productDTO.Name;
            existPr.SalePrice = productDTO.SalePrice;
            existPr.CostPrice = productDTO.CostPrice;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existPr = _context.Products.Find(id);

            if (existPr == null) return NotFound();

            _context.Products.Remove(existPr);
            _context.SaveChanges();

            return NoContent();
        }
    }


}
