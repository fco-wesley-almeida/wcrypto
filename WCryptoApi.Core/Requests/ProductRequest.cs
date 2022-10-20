using System.ComponentModel.DataAnnotations;

namespace WCryptoApi.Core;

public class ProductRequest
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 1)]
    public string Description { get; set; }
    [Required]
    [Range(minimum: 1, int.MaxValue)]
    public int CategoryId { get; set;}
}