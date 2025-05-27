using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.data;
using api.Dtos;
using api.Dtos.Comment;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<StockDto>> GetAllAsync()
        {
            var stocks = await _context.Stocks.Include(c => c.Comments).ToListAsync();

            return stocks.Select(stock => new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments?.Select(comment => new CommentDto
                {
                    Id = comment.Id,
                    Title = comment.Title,
                    Content = comment.Content,
                    CreatedOn = comment.CreatedOn,
                    StockId = comment.StockId
                }).ToList() ?? new List<CommentDto>()

            }
            ).ToList();
        }

        public async Task<StockDto?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null)
            {
                return null;
            }

            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments?.Select(comment => new CommentDto
                {
                    Id = comment.Id,
                    Title = comment.Title,
                    Content = comment.Content,
                    CreatedOn = comment.CreatedOn,
                    StockId = comment.StockId
                }).ToList() ?? new List<CommentDto>()

            };
        }

        public async Task<StockDto> CreateAsync(CreateStockRequestDto stockDto)
        {
            var stock = new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };

            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();

            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = new List<CommentDto>()
            };
        }

        public async Task<StockDto?> UpdateAsync(UpdateStockRequestDto stockDto)
        {
            var stock = await _context.Stocks.FindAsync(stockDto.Id);
            if (stock == null)
            {
                return null;
            }

            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = new List<CommentDto>()
            };
        }

        public async Task<StockDto?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }

            var stockDto = new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = new List<CommentDto>()
            };

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return stockDto;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}