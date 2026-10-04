using Microsoft.AspNetCore.Mvc;
using StoreClient.Models;
using StoreClient.Services;

namespace StoreClient.Controllers;

public class StoreProductController : Controller
{
    private readonly IStoreProductApiService _productService;

    public StoreProductController(
        IStoreProductApiService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();

        if (products.Count == 0)
        {
            ViewBag.Error =
                "Не удалось получить товары. " +
                "Проверьте, запущен ли Web API.";
        }

        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            ViewBag.Error = "Товар не найден.";
            return View();
        }

        return View(product);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        StoreProduct product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        var result =
            await _productService.CreateAsync(product);

        if (!result.Success)
        {
            ModelState.AddModelError(
                string.Empty,
                result.Error ??
                "Ошибка при создании товара.");

            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product =
            await _productService.GetByIdAsync(id);

        if (product == null)
        {
            TempData["Error"] =
                "Товар не найден.";

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        StoreProduct product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        var result =
            await _productService.UpdateAsync(product);

        if (!result.Success)
        {
            ModelState.AddModelError(
                string.Empty,
                result.Error ??
                "Ошибка при изменении товара.");

            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _productService.DeleteAsync(id);

        if (!result.Success)
        {
            TempData["Error"] =
                result.Error ??
                "Ошибка при удалении товара.";
        }

        return RedirectToAction(nameof(Index));
    }
}