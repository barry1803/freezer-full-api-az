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

    private const string DevEnvValue = "Development";
    private const string DbPath = "./Data/LocalDatabase.db";
    private const string AzureDbPath = "D:/home/LocalDatabase.db";
    private readonly bool _isDevEnv = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == DevEnvValue;

    private static void CopyDb()
    {
        File.Copy(DbPath, AzureDbPath);
        File.SetAttributes(AzureDbPath, FileAttributes.Normal);
    }

    public IDbConnection CreateConnection()
    {
        if (!_isDevEnv && !File.Exists(AzureDbPath)) CopyDb();
        return new SqliteConnection($"data source={(_isDevEnv ? DbPath : AzureDbPath)}");
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