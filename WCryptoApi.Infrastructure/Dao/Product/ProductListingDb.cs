using Dapper;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Product;
public class ProductListingDb: WCryptoDb, IProductListingDb
{
	private string? _sql;
    public Task<IEnumerable<ProductDto>> FindAllByCategoryId(int categoryId)
    {
        return AssembleSql()
              .ApplyCategoryIdFilter()
              .Connect()
              .QueryAsync<ProductDto>(_sql, new {
                 CategoryId = categoryId
              })
		;
    }

    public Task<IEnumerable<ProductDto>> FindAll()
    {
	    return AssembleSql()
          .Connect()
          .QueryAsync<ProductDto>(_sql)
	    ;
    }

    private ProductListingDb AssembleSql()
    {
	    _sql = @"
			select 
				p.id 			as ProductId
				,p.description 	as Description
				,p.category_id 	as CategoryId
				,c.description 	as CategoryDescription
				,c.user_id 		as UserId
			from product p
			inner join category c on p.category_id = c.id
			where 1
				and p.deleted = 0
				and c.deleted = 0
        ";
	    return this;
    }
    
    private ProductListingDb ApplyCategoryIdFilter()
    {
	    _sql += " and c.id = @CategoryId ";
	    return this;
    }
}