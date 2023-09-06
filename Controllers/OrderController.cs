using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.Models;
using BIS_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace BIS_project.Controllers;
[Route("api/v1/user/base-order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService service)
    {
        _orderService = service;
    }

    [HttpPost(Name = "CreateOrder")]
    public async Task<APIResponse> CreateOrder(BaseOrderDto order)
    {
        try
        {
            return await _orderService.CreateBaseOrder(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("get-all", Name = "GetAllOrders")]
    public async Task<List<Order>> GetAllOrders()
    {
        return await _orderService.GetAllOrders();
    }

   

}