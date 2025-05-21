using api.data;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Mappers;
using api.Dtos;
using api.Dtos.Stock;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stockDtos = stocks.Select(stock => stock.ToStockDto());
            return Ok(stockDtos);
        }
        // GET: api/stock/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        // POST: api/stock
        [HttpPost]
        public async Task <IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
           await _context.Stocks.AddAsync(stock);
           await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockDto());
        }


        // DELETE: api/stock/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
           await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/stock/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stockModel = await  _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.Divident = stockDto.Divident;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            stockModel.MarketCap = stockDto.MarketCap;

           _context.Stocks.Update(stockModel);
           await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }
    }

}