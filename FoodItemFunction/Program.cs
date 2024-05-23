using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        // configure DI for application services
        services.AddScoped<IFoodItemService, FoodItemService>();
        services.AddScoped<IFoodItemRepository, FoodItemRepository>();
        services.AddScoped<DataContext>();
    })
    .Build();


host.Run();
