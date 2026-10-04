using System.ComponentModel.DataAnnotations;

namespace StoreClient.Models;

public class StoreProduct
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название товара.")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Range(0, double.MaxValue,
        ErrorMessage = "Цена не может быть отрицательной.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue,
        ErrorMessage = "Количество не может быть отрицательным.")]
    public int Quantity { get; set; }
}