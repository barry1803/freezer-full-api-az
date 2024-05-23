using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebApi.Models.FoodItems;
using WebApi.Services;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace WebApi
{
    public class CreateFoodItem(ILogger<GetFoodItem> logger, IFoodItemService foodItemService)
    {
        [Function("CreateFoodItem")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "FoodItems")] HttpRequest req, [FromBody] CreateRequest model)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            await foodItemService.Create(model);
            return new OkObjectResult("Food item created");
        }
    }
}
