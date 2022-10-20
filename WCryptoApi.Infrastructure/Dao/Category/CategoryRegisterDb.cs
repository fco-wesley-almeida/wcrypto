using System.Data;
using Dapper;
using WCryptoApi.Core.Data.Category;
using Entities = WCryptoApi.Core.Entities;

namespace WCryptoApi.Infrastructure.Dao.Category;
public class CategoryRegisterDb: WCryptoDb, ICategoryRegisterDb
{
	private string? _sql;

	public async Task<int> Register(Entities.Category category)
	{
		IDbConnection connection = Connect();
		AssembleSql();
		await connection.ExecuteAsync(_sql, category);
		return await connection.QueryFirstAsync<int>(sql: "select LAST_INSERT_ID()");
	}
    
    private CategoryRegisterDb AssembleSql()
    {
	    _sql = @"
			insert into category (description, user_id, deleted) 
			values (
				@Description,
			    @UserId,
			    0
			)
        ";
	    return this;
    }
}