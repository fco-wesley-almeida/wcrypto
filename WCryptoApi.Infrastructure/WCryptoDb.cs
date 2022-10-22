using System.Data;
using MySql.Data.MySqlClient;
using WCryptoApi.Core.ApplicationModels;

namespace WCryptoApi.Infrastructure;

public class WCryptoDb
{
    private readonly string _dbConnectionString;

    protected WCryptoDb()
    {
        EnvironmentVariable connectionStrEnvVar = new EnvironmentVariable("DB_CONNECTION");
        _dbConnectionString = connectionStrEnvVar.Value;
    }

    protected IDbConnection Connect()
    {
        return new MySqlConnection(_dbConnectionString);
    }
}