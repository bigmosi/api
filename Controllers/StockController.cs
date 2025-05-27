using api.Dtos;
using api.Dtos.Stock;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            return Ok(stocks);
        }

        // GET: api/stock/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // POST: api/stock
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var createdStock = await _stockRepo.CreateAsync(stockDto);
            return CreatedAtAction(nameof(GetStockById), new { id = createdStock.Id }, createdStock);
        }

        // PUT: api/stock/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (id <= 0)
                return BadRequest("Invalid stock ID.");

            stockDto.Id = id; // Set ID since it might not be in body

            var updatedStock = await _stockRepo.UpdateAsync(stockDto);
            if (updatedStock == null)
            {
                return NotFound();
            }

            return Ok(updatedStock);
        }

        // DELETE: api/stock/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var deletedStock = await _stockRepo.DeleteAsync(id);
            if (deletedStock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
