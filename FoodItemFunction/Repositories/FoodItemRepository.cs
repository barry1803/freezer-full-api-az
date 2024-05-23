namespace WebApi.Repositories;

using Dapper;
using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Helpers;

public interface IFoodItemRepository
{
    Task<IEnumerable<FoodItem>> GetAll();
    Task<FoodItem> GetById(int? id);
    Task<IEnumerable<FoodItem>> GetByName(string search);
    Task Create(FoodItem foodItem);
    Task Update(FoodItem foodItem);
    Task Delete(int id);
}

public class FoodItemRepository(DataContext context) : IFoodItemRepository
{
    public async Task<IEnumerable<FoodItem>> GetAll()
    {
        using var connection = context.CreateConnection();
        var sql = """
            SELECT * FROM FoodItems Order By Name
        """;
        return await connection.QueryAsync<FoodItem>(sql);
    }

    public async Task<FoodItem> GetById(int? id)
    {
        using var connection = context.CreateConnection();
        var sql = """
            SELECT * FROM FoodItems 
            WHERE Id = @id
        """;
        var result = await connection.QuerySingleOrDefaultAsync<FoodItem>(sql, new { id });
        return result;
    }

    public async Task<IEnumerable<FoodItem>> GetByName(string search)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", "%" + search + "%");
        using var connection = context.CreateConnection();
        var sql = "SELECT * FROM FoodItems WHERE Name LIKE @Name Order By Name";
        var result = await connection.QueryAsync<FoodItem>(sql, parameters);
        return result;
    }

    public async Task Create(FoodItem foodItem)
    {
        using var connection = context.CreateConnection();
        var sql = """
            INSERT INTO FoodItems (Name, Description, DateFrozen, Quantity, FreezerLocation, ItemLocation)
            VALUES (@Name, @Description, @DateFrozen, @Quantity, @FreezerLocation, @ItemLocation)
        """;
        await connection.ExecuteAsync(sql, foodItem);
    }

    public async Task Update(FoodItem FoodItem)
    {
        using var connection = context.CreateConnection();
        var sql = """
            UPDATE FoodItems 
            SET Name = @Name,
                Description = @Description,
                DateFrozen = @DateFrozen,
                Quantity = @Quantity, 
                FreezerLocation = @FreezerLocation, 
                ItemLocation = @ItemLocation 
            WHERE Id = @Id
        """;
        await connection.ExecuteAsync(sql, FoodItem);
    }

    public async Task Delete(int id)
    {
        using var connection = context.CreateConnection();
        var sql = """
            DELETE FROM FoodItems 
            WHERE Id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}