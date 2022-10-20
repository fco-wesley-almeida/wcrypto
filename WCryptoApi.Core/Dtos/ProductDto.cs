namespace WCryptoApi.Core.Dtos;

public class ProductDto
{
    public int    ProductId           { get; set; }
    public string Description         { get; set; } = null!;
    public int    CategoryId          { get; set; }          
    public string CategoryDescription { get; set; } = null!;
    public int    UserId              { get; set; }
}