using Dapper;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Category;
public class CategoryListingDb: WCryptoDb, ICategoryListingDb
{
	private string? _sql;
    public Task<IEnumerable<CategoryDto>> FindAllByUserId(int userId)
    {
        return AssembleSql()
              .ApplyUserIdFilter()
              .Connect()
              .QueryAsync<CategoryDto>(_sql, new {
                 UserId = userId
              })
		;
    }

    public Task<IEnumerable<CategoryDto>> FindAll()
    {
	    return AssembleSql()
          .Connect()
          .QueryAsync<CategoryDto>(_sql)
	    ;
    }

    private CategoryListingDb AssembleSql()
    {
	    _sql = @"
			select 
				 c.id 			as CategoryId
				,c.description 	as Description
				,c.user_id 		as UserId
			from category c
			where c.deleted = 0
        ";
	    return this;
    }
    
    private CategoryListingDb ApplyUserIdFilter()
    {
	    _sql += " and c.user_id = @UserId ";
	    return this;
    }
}