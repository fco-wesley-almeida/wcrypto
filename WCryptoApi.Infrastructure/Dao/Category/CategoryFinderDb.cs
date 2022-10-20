using Dapper;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Category;
public class CategoryFinderDb: WCryptoDb, ICategoryFinderDb
{
	private string? _sql;

	public async Task<CategoryDto?> FindById(int categoryId)
    {
	    AssembleSql();
	    return await Connect().QueryFirstOrDefaultAsync<CategoryDto>(_sql, new
	    {
		    CategoryId = categoryId
	    });
    }
    
    private void AssembleSql()
    {
	    _sql = @"
			select 
				 c.id 			as CategoryId
				,c.description 	as Description
				,c.user_id 		as UserId
			from category c
			where
				c.id = @CategoryId
				and c.deleted = 0
        ";
    }
}