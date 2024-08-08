using EasyPassportImage.Interfaces;
using EasyPassportImage.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyPassportImage.Controllers;

[ApiController]
[Route("/api/passport/")]
public class PassportController : ControllerBase
{
    private readonly IPassportOrderService _service;

    public PassportController(IPassportOrderService service)
    {
        _service = service;
    }

    [HttpPost("createOrder")]
    public Task<IResult> CreateOrder([FromForm] Passport passport)
    {
        return _service.CreateOrderAsync(passport);
    }

    [HttpGet("getOrder")]
    public Task<IResult> FetchOrder([FromQuery] string id)
    {
        return _service.GetItemAsync(id);
    }
}
