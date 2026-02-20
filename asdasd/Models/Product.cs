using System.ComponentModel.DataAnnotations;
namespace asdasd.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Наименованието е задължително.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Името трябва да е между 3 и 100 символа.")]
    [Display(Name = "Наименование")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Цената е задължителна.")]
    [Range(0.01, 100000, ErrorMessage = "Цената трябва да бъде между 0.01 и 100 000.")]
    [DataType(DataType.Currency)]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }
}