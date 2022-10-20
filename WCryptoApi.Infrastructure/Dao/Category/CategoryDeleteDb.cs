using Dapper;
using WCryptoApi.Core.Data.Category;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Category;
public class CategoryDeleteDb: WCryptoDb, ICategoryDeleteDb
{
	private string? _sql;

	public async Task<bool> Delete(int categoryId)
	{
		int rowsAffected = await AssembleSql()
	        .Connect()
	        .ExecuteAsync(_sql, new {
	             CategoryId = categoryId
	         })
		;
		return rowsAffected > 0;
	}
    
    private CategoryDeleteDb AssembleSql()
    {
	    _sql = @"
			update 
				category
			set 
				deleted = 1
			where
			    id = @CategoryId
				and deleted = 0
        ";
	    return this;
    }

}