using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Repositories;

namespace WebApi.Helpers;

using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

public class DataContext(ILogger<FoodItemRepository> logger, IConfiguration configuration)
{
    protected readonly IConfiguration Configuration = configuration;

    public IDbConnection CreateConnection()
    {
        var connectionString = Configuration.GetConnectionString("CUSTOMCONNSTR_WebApiDatabase");
        logger.LogInformation($"Connection string: {connectionString}");
        return new SqliteConnection("Data Source = Data\\LocalDatabase.db");
    }

    public async Task Init()
    {
        // create database tables if they don't exist
        using var connection = CreateConnection();
        await _initFoodItems();

        async Task _initFoodItems()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                FoodItems (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Name TEXT COLLATE NOCASE,
                    Description TEXT COLLATE NOCASE,
                    DateFrozen TEXT,
                    Quantity REAL,
                    FreezerLocation TEXT,
                    ItemLocation TEXT
                );
            """;
            await connection.ExecuteAsync(sql);
        }
    }
}