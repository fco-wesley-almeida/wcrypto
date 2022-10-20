using Dapper;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Product;
public class ProductFinderDb: WCryptoDb, IProductFinderDb
{
	private string? _sql;

	public async Task<ProductDto?> FindById(int productId)
    {
	    AssembleSql();
	    return await Connect().QueryFirstOrDefaultAsync<ProductDto>(_sql, new
	    {
		    ProductId = productId
	    });
    }
    
    private void AssembleSql()
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
			where
				p.id = @ProductId
				and p.deleted = 0
				and c.deleted = 0
        ";
    }
}