using BIS_project.Dtos;
using BIS_project.Models;

namespace BIS_project.IServices;

public interface IOrderService
{
    public Task<List<Order>> GetAllOrders();
    public Task<Order> GetOrderById(int id);
    public Task<object> CreateBaseOrder(BaseOrderDto order);
}