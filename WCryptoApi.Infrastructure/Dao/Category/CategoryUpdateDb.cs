using Dapper;
using WCryptoApi.Core.Data.Category;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Category;
public class CategoryUpdateDb: WCryptoDb, ICategoryUpdateDb
{
	private string? _sql;

	public async Task<bool> Update(Entities.Category category)
	{
		int rowsAffected = await AssembleSql()
	        .Connect()
	        .ExecuteAsync(_sql, category)
		;
		return rowsAffected > 0;
	}
    
    private CategoryUpdateDb AssembleSql()
    {
	    _sql = @"
			update 
				category
			set 
				description = @Description,
				user_id = @UserId
			where
			    id = @CategoryId
				and deleted = 0
        ";
	    return this;
    }
}