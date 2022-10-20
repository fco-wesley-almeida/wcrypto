namespace WCryptoApi.Core.Dtos;

public class CategoryDto
{
    public int    CategoryId  { get; set; }
    public string Description { get; set;  } = null!;
    public int     UserId      { get; set;  }
}