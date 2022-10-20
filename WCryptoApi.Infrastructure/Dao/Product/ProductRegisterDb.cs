using System.Data;
using Dapper;
using WCryptoApi.Core.Data.Product;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Product;
public class ProductRegisterDb: WCryptoDb, IProductRegisterDb
{
	private string? _sql;

	public async Task<int> Register(Entities.Product product)
	{
		IDbConnection connection = Connect();
		AssembleSql();
		await connection.ExecuteAsync(_sql, product);
		return await connection.QueryFirstAsync<int>(sql: "select LAST_INSERT_ID()");
	}
    
    private ProductRegisterDb AssembleSql()
    {
	    _sql = @"
			insert into product (description, category_id, deleted)
			values (
				@Description,
			    @CategoryId,
			    0
			)
        ";
	    return this;
    }
}