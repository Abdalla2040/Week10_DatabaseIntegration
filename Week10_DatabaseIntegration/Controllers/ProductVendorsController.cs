#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week10_DatabaseIntegration.Data;
using Week10_DatabaseIntegration.Models;

namespace Week10_DatabaseIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVendorsController : ControllerBase
    {
        private readonly Adventureworks2019Context _context;

        public ProductVendorsController(Adventureworks2019Context context)
        {
            _context = context;
        }

        // GET: api/ProductVendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVendor>>> GetProductVendors()
        {
            return await _context.ProductVendors.ToListAsync();
        }

        // GET: api/ProductVendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVendor>> GetProductVendor(int id)
        {
            var productVendor = await _context.ProductVendors.FindAsync(id);

            if (productVendor == null)
            {
                return NotFound();
            }

            return productVendor;
        }

        // PUT: api/ProductVendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductVendor(int id, ProductVendor productVendor)
        {
            if (id != productVendor.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(productVendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductVendorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductVendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductVendor>> PostProductVendor(ProductVendor productVendor)
        {
            _context.ProductVendors.Add(productVendor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductVendorExists(productVendor.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductVendor", new { id = productVendor.ProductId }, productVendor);
        }

        // DELETE: api/ProductVendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductVendor(int id)
        {
            var productVendor = await _context.ProductVendors.FindAsync(id);
            if (productVendor == null)
            {
                return NotFound();
            }

            _context.ProductVendors.Remove(productVendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductVendorExists(int id)
        {
            return _context.ProductVendors.Any(e => e.ProductId == id);
        }
    }
}
