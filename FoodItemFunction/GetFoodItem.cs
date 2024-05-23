using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebApi.Services;

namespace WebApi
{
    public class GetFoodItem(ILogger<GetFoodItem> logger, IFoodItemService foodItemService)
    {
        [Function("GetFoodItem")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "FoodItems/{id?}")] HttpRequest req, int? id)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            if (string.IsNullOrEmpty(id.ToString()))
            {
                var foodItems = await foodItemService.GetAll();
                return new OkObjectResult(foodItems);
            }
            var foodItem = await foodItemService.GetById(id);
            return new OkObjectResult(foodItem);
        }
    }
}
