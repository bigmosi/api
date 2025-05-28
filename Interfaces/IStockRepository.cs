using api.Models;
using stock_api.Helpers;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(Stock stock);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
