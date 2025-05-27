using api.Dtos;
using api.Dtos.Stock;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetAllAsync();
        Task<StockDto?> GetByIdAsync(int id);

        Task<StockDto> CreateAsync(CreateStockRequestDto stockDto);
        Task<StockDto?> UpdateAsync(UpdateStockRequestDto stockDto);
        Task<StockDto?> DeleteAsync(int id);

        Task<bool> StockExists(int id);

    }
}