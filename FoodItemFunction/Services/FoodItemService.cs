namespace WebApi.Services;
using WebApi.Entities;
using WebApi.Models.FoodItems;
using WebApi.Repositories;

public interface IFoodItemService
{
    Task<IEnumerable<FoodItem>> GetAll();
    Task<FoodItem> GetById(int? id);
    Task Create(CreateRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}

public class FoodItemService(IFoodItemRepository foodItemRepository)
    : IFoodItemService
{
    public async Task<IEnumerable<FoodItem>> GetAll()
    {
        return await foodItemRepository.GetAll();
    }

    public async Task<FoodItem> GetById(int? id)
    {
        var foodItem = await foodItemRepository.GetById(id);

        return foodItem ?? new FoodItem();
    }

    public async Task Create(CreateRequest model)
    {
        // map model to new FoodItem object
        var foodItem = new FoodItem
        {
            Name = model.Name,
            Description = model.Description,
            DateFrozen = model.DateFrozen,
            Quantity = (int)model.Quantity,
            FreezerLocation = model.FreezerLocation,
            ItemLocation = model.ItemLocation
        };

        if (string.IsNullOrEmpty(foodItem.DateFrozen)) foodItem.DateFrozen = DateTime.Now.ToString("yyyy-MM-dd");

        // save FoodItem
        await foodItemRepository.Create(foodItem);
    }

    public async Task Update(int id, UpdateRequest model)
    {
        var foodItem = await foodItemRepository.GetById(id) ?? throw new KeyNotFoundException("FoodItem not found");

        // copy model props to FoodItem
        foodItem.Name = model.Name ?? foodItem.Name;
        foodItem.Description = model.Description ?? foodItem.Description;
        foodItem.DateFrozen = model.DateFrozen ?? foodItem.DateFrozen;
        foodItem.Quantity = (int)(model.Quantity ?? foodItem.Quantity);
        foodItem.FreezerLocation = model.FreezerLocation ?? foodItem.FreezerLocation;
        foodItem.ItemLocation = model.ItemLocation ?? foodItem.ItemLocation;

        // save FoodItem
        await foodItemRepository.Update(foodItem);
    }

    public async Task Delete(int id)
    {
        await foodItemRepository.Delete(id);
    }
}