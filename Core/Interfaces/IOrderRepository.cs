using FinekraApi.Core.Entities;
using System.Collections.Generic;

namespace FinekraApi.Core.Interfaces
{
    public interface IOrderRepository
    {
        Orders AddOrder(Orders order);
        void UpdateOrder(Orders order);
        Orders GetOrderById(int orderId);
        IEnumerable<Orders> GetAllOrders();
        void RemoveOrder(Orders order);
    }
}
