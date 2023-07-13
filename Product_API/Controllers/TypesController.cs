using Microsoft.AspNetCore.Mvc;
using Product_API.Data;
using Product_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Product_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TypesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var typeList = _context.Types.ToList();
                return Ok(typeList);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var type = _context.Types.SingleOrDefault(t => t.TypeId == id);
            if (type != null)
            {
                return Ok(type);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateNew(TypeModel model)
        {
            try
            {
                var type = new Product_API.Data.Type
                {
                    TypeName = model.TypeName
                };
                _context.Add(type);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, type);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, TypeModel model)
        {
            var type = _context.Types.SingleOrDefault(t => t.TypeId == id);
            if (type != null)
            {
                type.TypeName = model.TypeName;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoaiById(int id)
        {
            var type = _context.Types.SingleOrDefault(t => t.TypeId == id);
            if (type != null)
            {
                _context.Remove(type);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
