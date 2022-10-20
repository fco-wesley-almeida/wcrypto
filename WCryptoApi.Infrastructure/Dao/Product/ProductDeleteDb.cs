using Dapper;
using WCryptoApi.Core.Data.Product;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Product;
public class ProductDeleteDb: WCryptoDb, IProductDeleteDb
{
	private string? _sql;

	public async Task<bool> Delete(int productId)
	{
		int rowsAffected = await AssembleSql()
	        .Connect()
	        .ExecuteAsync(_sql, new {
	             ProductId = productId
	         })
		;
		return rowsAffected > 0;
	}
    
    private ProductDeleteDb AssembleSql()
    {
	    _sql = @"
			update 
				product
			set 
				deleted = 1
			where
			    id = @ProductId
				and deleted = 0
        ";
	    return this;
    }

}