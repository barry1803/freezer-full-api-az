using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebApi.Services;

namespace WebApi
{
    public class DeleteFoodItem(ILogger<GetFoodItem> logger, IFoodItemService foodItemService)
    {

        [Function("DeleteFoodItem")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "FoodItems/{id?}")] HttpRequest req, int id)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            foodItemService.Delete(id);
            return new OkObjectResult("Food item deleted");
        }
    }
}
