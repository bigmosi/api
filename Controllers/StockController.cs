using api.Dtos;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using stock_api.Helpers;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        // GET: api/stock
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetAllAsync(query);
            return Ok(stocks);
        }

        // GET: api/stock/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        // POST: api/stock
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };

            var createdStock = await _stockRepo.CreateAsync(stock);
            return CreatedAtAction(nameof(GetStockById), new { id = createdStock.Id }, createdStock);
        }

        // PUT: api/stock/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest("Invalid stock ID.");

            var stock = new Stock
            {
                Id = id,
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };

            var updatedStock = await _stockRepo.UpdateAsync(stock);
            if (updatedStock == null)
                return NotFound();

            return Ok(updatedStock);
        }

        // DELETE: api/stock/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedStock = await _stockRepo.DeleteAsync(id);
            if (deletedStock == null)
                return NotFound();

            return NoContent();
        }
    }
}
