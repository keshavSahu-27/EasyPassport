using EasyPassportImage.Models;

namespace EasyPassportImage.Interfaces;

public interface IPassportOrderService
{
    Task<IResult> CreateOrderAsync(Passport passport);
    Task<IResult> GetItemAsync(string id);
}