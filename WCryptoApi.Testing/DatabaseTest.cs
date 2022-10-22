using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Xunit.Abstractions;

namespace WCryptoApi.Testing;

public class DatabaseTest<T>: UnitTestBase<T>
{
	public DatabaseTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
	{
	}
	
    protected async Task CreatePreConditionsForTesting()
    {
        const string sql = @"
			delete from product where 1;
			delete from category where 1;

			insert into category (id, description,user_id,deleted) values
				 (1, 'category test 1',1,0),
				 (2, 'category test 2',1,0),
				 (3, 'category test 3',1,1),
				 (4, 'category test 4',1,0),
				 (5, 'category test 5',1,0)
			 ;

			insert into product (id, description,category_id,deleted) values
				 (1, 'category test 1',1,0),
				 (2, 'category test 2',2,0),
				 (3, 'category test 3',2,1),
				 (4, 'category test 4',3,0),
				 (5, 'category test 5',3,1),
				 (6, 'category test 6',3,0),
				 (7, 'category test 7',4,0),
				 (8, 'category test 8',4,0)
			 ;
        ";
        Environment.SetEnvironmentVariable("DB_CONNECTION", "Server=localhost;Port=7462;Database=wcripto;Uid=wmeida;Pwd=IL*C^9XrY^InGs&$wPmnFX(9K6ZKVI");
        IDbConnection connection = new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));
        if (await connection.ExecuteAsync(sql) == 0)
        {
	        throw new Exception("Failure on creating database pre conditions testing.");
        };
    }
}