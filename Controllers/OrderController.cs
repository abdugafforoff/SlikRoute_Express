using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.Models;
using BIS_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System;

namespace BIS_project.Controllers;
[Route("api/v1/base-order")]
[ApiController]
public class OrderController : ControllerBase
{
    public string comment { get; set; } = string.Empty;

    private readonly OrderService _orderService;
    private readonly FileSaver _fileSaver;
    public OrderController(OrderService service, FileSaver saver)
    {
        _orderService = service;
        _fileSaver = saver;
    }

    [HttpPost("user", Name = "CreateOrder")]
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
    [HttpPut("user/update", Name = "GenerateOrder")]
    public async Task<IActionResult> GenerateOrder()
    {
        try
        {
            var orderData = JsonConvert.DeserializeObject<UpdateOrderDto>(Request.Form["order"]);
            List<Image> images = await _fileSaver.UploadFiles(Request.Form.Files, "orders/images");
            var result = await _orderService.UpdateOrder(orderData, images);
            if (result == null)
            {
                return BadRequest("Orders not found");
            }
            var res = Ok(result);
            return res;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get-all", Name = "GetAllOrders")]
    public async Task<List<Order>?> GetAllOrders()
    {
        return await _orderService.GetAllOrders();
    }

   

}