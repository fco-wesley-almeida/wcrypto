using Dapper;
using WCryptoApi.Core.Data.Product;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Product;
public class ProductUpdateDb: WCryptoDb, IProductUpdateDb
{
	private string? _sql;

	public async Task<bool> Update(Entities.Product product)
	{
		int rowsAffected = await AssembleSql()
	        .Connect()
	        .ExecuteAsync(_sql, product)
		;
		return rowsAffected > 0;
	}
    
    private ProductUpdateDb AssembleSql()
    {
	    _sql = @"
			update 
				product
			set 
				description = @Description,
				category_id = @CategoryId
			where
			    id = @ProductId
				and deleted = 0
        ";
	    return this;
    }
}