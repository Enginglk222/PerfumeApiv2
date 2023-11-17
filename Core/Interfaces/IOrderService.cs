using FinekraApi.Core.Entities;
using System.Collections.Generic;

namespace FinekraApi.Core.Interfaces
{
    public interface IOrderService
    {
        Orders CreateOrder(int userDetailId, string shipAddress, int phone, List<OrderDetails> orderDetails);
        void AddToCart(Orders order, OrderDetails orderDetail);
        void UpdateCartItem(Orders order, int orderDetailsId, int count);
        void RemoveFromCart(Orders order, int orderDetailsId);
        Orders GetOrderById(int orderId);
        IEnumerable<Orders> GetAllOrders();
       
    }
}
