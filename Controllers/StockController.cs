using api.data;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Mappers;

namespace api.Controllers
{

    [Route("api/stock")]
    [ApiController]
    // This controller is responsible for handling stock-related operations.
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        // GET: api/stock
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
                .Select(s => s.ToStockDto());
            return Ok(stocks);
        }
        // GET: api/stock/{id}
        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        // POST: api/stock
        [HttpPost]
        public IActionResult CreateStock([FromBody] Stock stock)
        {
            if (stock == null)
            {
                return BadRequest();
            }
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock);
        }


        // DELETE: api/stock/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteStock(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return NoContent();
        }
    }

}