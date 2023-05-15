using API.Apps.AdminApi.DTOs.BrandDTOs;
using Core.Entites;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public BrandsController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var brands = _context.Brands.ToList();

            List<BrandGetAllDTO> getAllDTO = brands.Select(x => new BrandGetAllDTO { Id = x.Id, Name = x.Name }).ToList();

            return Ok(getAllDTO);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _context.Brands.Include(x=>x.Products).FirstOrDefault(x=>x.Id==id);

            if (data == null) return NotFound();

            BrandGetDTO brandGetDTO = new() { Id=data.Id,Name=data.Name, ProductCount=data.Products.Count };

            return Ok(brandGetDTO);
        }

        [HttpPost("")]
        public IActionResult Create(BrandDTO brandDTO)
        {
            if (_context.Brands.Any(x => x.Name == brandDTO.Name))
            {
                ModelState.AddModelError("Name", "This brand name is already in use");
                return BadRequest(ModelState);
            }

            Brand brand = new() { Name = brandDTO.Name };

            _context.Brands.Add(brand);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, brand);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BrandDTO brandDTO)
        {
            var existBrand = _context.Brands.Find(id);

            if (existBrand == null) return NotFound();

            if (existBrand.Name != brandDTO.Name && _context.Brands.Any(x => x.Name == brandDTO.Name))
            {
                ModelState.AddModelError("Name", "This name is already in use");
                return BadRequest(ModelState);
            }

            existBrand.Name = brandDTO.Name;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existBrand = _context.Brands.Find(id);

            if (existBrand == null) return NotFound();

            _context.Brands.Remove(existBrand);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
