using System.Diagnostics;
using EasyPassportImage.Enums;
using EasyPassportImage.Interfaces;
using EasyPassportImage.Models;

namespace EasyPassportImage.Services;
public class PassportOrderService : IPassportOrderService
{
    public readonly IDynamoDbService<Order> _service;
    public PassportOrderService(IDynamoDbService<Order> service)
    {
        _service = service;
    }

    public async Task<IResult> CreateOrderAsync(Passport passport)
    {
        try
        {
            // ToDo: Save passport.Image to S3 and get the url and assign it to imageUrl
            var imageUrl = "https://pixy.org/src/31/315160.png";

            var order = new Order {
                Id = Guid.NewGuid().ToString(),
                CustomerName = passport.CustomerName,
                DeliveryDate = DateTime.Now.AddDays(5),
                OrderDate = DateTime.Now,
                ImageUrl = imageUrl,
                State = State.Placed
            };

            var result = await _service.SaveItemAsync(order);

            if(!result) return Results.Json("Internal server error",
                statusCode: StatusCodes.Status500InternalServerError);

            return Results.Ok("Success");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error Occured", ex);
            return Results.Json("Internal server error", 
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IResult> GetItemAsync(string id)
    {
        try
        {
            var result = await _service.GetItemAsync(id);

            if(result is null) return Results.Json("Internal server error", 
                statusCode: StatusCodes.Status500InternalServerError);

            result.Id =  Guid.NewGuid().ToString();
            result.DeliveryDate = DateTime.Now.AddDays(5);
            result.CustomerName = "AWS POC";
            result.OrderDate = DateTime.Now;
            result.ImageUrl = "https://pixy.org/src/31/315160.png";
            result.State = State.Accepted;
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error Occured", ex);
            return Results.Json("Internal server error", 
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}