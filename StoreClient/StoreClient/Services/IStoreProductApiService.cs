using StoreClient.Models;

namespace StoreClient.Services;

public interface IStoreProductApiService
{
    Task<List<StoreProduct>> GetAllAsync();

    Task<StoreProduct?> GetByIdAsync(int id);

    Task<(bool Success, StoreProduct? Product, string? Error)>
        CreateAsync(StoreProduct product);

    Task<(bool Success, string? Error)>
        UpdateAsync(StoreProduct product);

    Task<(bool Success, string? Error)>
        DeleteAsync(int id);
}