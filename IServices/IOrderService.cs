using BIS_project.Dtos;
using BIS_project.Helper;
using BIS_project.Models;

namespace BIS_project.IServices;

public interface IOrderService
{
    public Task<List<Order>> GetAllOrders();
    public Task<Order> GetOrderById(int id);
    public Task<APIResponse> CreateBaseOrder(BaseOrderDto order);
}