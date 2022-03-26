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
    public class ProductInventoriesController : ControllerBase
    {
        private readonly Adventureworks2019Context _context;

        public ProductInventoriesController(Adventureworks2019Context context)
        {
            _context = context;
        }

        // GET: api/ProductInventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInventory>>> GetProductInventories()
        {
            return await _context.ProductInventories.ToListAsync();
        }

        // GET: api/ProductInventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInventory>> GetProductInventory(int id)
        {
            var productInventory = await _context.ProductInventories.FindAsync(id);

            if (productInventory == null)
            {
                return NotFound();
            }

            return productInventory;
        }

        // PUT: api/ProductInventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInventory(int id, ProductInventory productInventory)
        {
            if (id != productInventory.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(productInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInventoryExists(id))
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

        // POST: api/ProductInventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInventory>> PostProductInventory(ProductInventory productInventory)
        {
            _context.ProductInventories.Add(productInventory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductInventoryExists(productInventory.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductInventory", new { id = productInventory.ProductId }, productInventory);
        }

        // DELETE: api/ProductInventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInventory(int id)
        {
            var productInventory = await _context.ProductInventories.FindAsync(id);
            if (productInventory == null)
            {
                return NotFound();
            }

            _context.ProductInventories.Remove(productInventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductInventoryExists(int id)
        {
            return _context.ProductInventories.Any(e => e.ProductId == id);
        }
    }
}
