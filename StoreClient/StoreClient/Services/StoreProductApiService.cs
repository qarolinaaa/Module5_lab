using System.Net;
using System.Net.Http.Json;
using StoreClient.Models;

namespace StoreClient.Services;

public class StoreProductApiService : IStoreProductApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StoreProductApiService(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient CreateClient()
    {
        return _httpClientFactory.CreateClient("StoreApi");
    }

    public async Task<List<StoreProduct>> GetAllAsync()
{
    var client = CreateClient();

    try
    {
        var response = await client.GetAsync("api/products");

        Console.WriteLine(
            $"API STATUS: {(int)response.StatusCode}");

        var content =
            await response.Content.ReadAsStringAsync();

        Console.WriteLine(
            $"API RESPONSE: {content}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return System.Text.Json.JsonSerializer
                .Deserialize<List<StoreProduct>>(content)
                ?? new List<StoreProduct>();
        }

        return new List<StoreProduct>();
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"API ERROR: {ex.Message}");

        return new List<StoreProduct>();
    }
}

    public async Task<StoreProduct?> GetByIdAsync(int id)
    {
        var client = CreateClient();

        try
        {
            var response = await client.GetAsync(
                $"api/products/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content
                    .ReadFromJsonAsync<StoreProduct>();
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<(
        bool Success,
        StoreProduct? Product,
        string? Error)> CreateAsync(
            StoreProduct product)
    {
        var client = CreateClient();

        try
        {
            var response = await client.PostAsJsonAsync(
                "api/products",
                product);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var createdProduct =
                    await response.Content
                        .ReadFromJsonAsync<StoreProduct>();

                return (true, createdProduct, null);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return (
                    false,
                    null,
                    "Некорректные данные товара.");
            }

            if ((int)response.StatusCode >= 500)
            {
                return (
                    false,
                    null,
                    "Ошибка сервера API.");
            }

            return (
                false,
                null,
                $"Ошибка API: {(int)response.StatusCode}");
        }
        catch
        {
            return (
                false,
                null,
                "Не удалось подключиться к Web API.");
        }
    }

    public async Task<(
        bool Success,
        string? Error)> UpdateAsync(
            StoreProduct product)
    {
        var client = CreateClient();

        try
        {
            var response = await client.PutAsJsonAsync(
                $"api/products/{product.Id}",
                product);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return (true, null);
            }

            if (response.StatusCode ==
                HttpStatusCode.NotFound)
            {
                return (false, "Товар не найден.");
            }

            if (response.StatusCode ==
                HttpStatusCode.BadRequest)
            {
                return (
                    false,
                    "Некорректные данные товара.");
            }

            if ((int)response.StatusCode >= 500)
            {
                return (
                    false,
                    "Ошибка сервера API.");
            }

            return (
                false,
                $"Ошибка API: {(int)response.StatusCode}");
        }
        catch
        {
            return (
                false,
                "Не удалось подключиться к Web API.");
        }
    }

    public async Task<(
        bool Success,
        string? Error)> DeleteAsync(int id)
    {
        var client = CreateClient();

        try
        {
            var response = await client.DeleteAsync(
                $"api/products/{id}");

            if (response.StatusCode ==
                    HttpStatusCode.NoContent ||
                response.StatusCode ==
                    HttpStatusCode.OK)
            {
                return (true, null);
            }

            if (response.StatusCode ==
                HttpStatusCode.NotFound)
            {
                return (false, "Товар не найден.");
            }

            if ((int)response.StatusCode >= 500)
            {
                return (
                    false,
                    "Ошибка сервера API.");
            }

            return (
                false,
                $"Ошибка API: {(int)response.StatusCode}");
        }
        catch
        {
            return (
                false,
                "Не удалось подключиться к Web API.");
        }
    }
}