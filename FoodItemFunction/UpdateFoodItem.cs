using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebApi.Models.FoodItems;
using WebApi.Services;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace WebApi
{
    public class UpdateFoodItem(ILogger<GetFoodItem> logger, IFoodItemService foodItemService)
    {

        [Function("UpdateFoodItem")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequest req, [FromBody] UpdateRequest model, int id)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            await foodItemService.Update(id, model);
            return new OkObjectResult("Food item updated");
        }
    }
}
