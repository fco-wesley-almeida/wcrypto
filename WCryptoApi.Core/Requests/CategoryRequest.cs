using System.ComponentModel.DataAnnotations;

namespace WCryptoApi.Core.Requests;

public class CategoryRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Esse campo deve ter até 100 caracteres.")]
    public string Description { get; set;  } = null!;

    [Required]
    [Range(minimum:1, maximum:int.MaxValue)]
    public int     UserId      { get; set;  }
}